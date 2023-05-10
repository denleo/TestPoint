using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TestPoint.Application.TestAssignments.Commands.CreateTestAssignment;
using TestPoint.Application.TestAssignments.Commands.CreateTestAssignmentByGroup;
using TestPoint.Application.TestAssignments.Commands.DeleteTestAssignment;
using TestPoint.Application.Tests;
using TestPoint.Application.Tests.Commands.CreateTest;
using TestPoint.Application.Tests.Commands.DeleteTest;
using TestPoint.Application.Tests.Commands.SubmitTestResult;
using TestPoint.Application.Tests.Queries.GetTestById;
using TestPoint.Application.Tests.Queries.GetTestResult;
using TestPoint.Application.Tests.Queries.GetTestsByAdmin;
using TestPoint.Application.Tests.Queries.GetTestsByUser;
using TestPoint.Application.Tests.Queries.GetUsersOnTest;
using TestPoint.Domain;
using TestPoint.WebAPI.Middlewares.CustomExceptionHandler;
using TestPoint.WebAPI.Models.Test;
using TestPoint.WebAPI.Models.TestCompletion;

namespace TestPoint.WebAPI.Controllers.Test;

public class TestController : BaseController
{
    [SwaggerOperation(Summary = "Create new test (role:admin)")]
    [HttpPost("tests"), Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Domain.Test>> CreateNewTest([FromBody] TestDto newTest)
    {
        var createTestCommand = new CreateTestCommand() // add mapper
        {
            AuthorId = LoginId!.Value,
            Name = newTest.Name,
            Difficulty = newTest.Difficulty,
            EstimatedTime = newTest.EstimatedTime,
            Questions = newTest.Questions.Select(x => new Question
            {
                QuestionText = x.QuestionText,
                QuestionType = x.QuestionType,
                Answers = x.Answers.Select(x => new Answer
                {
                    AnswerText = x.AnswerText,
                    IsCorrect = x.IsCorrect
                }).ToArray()
            }).ToArray()
        };

        return await Mediator.Send(createTestCommand);
    }

    [SwaggerOperation(Summary = "Get all tests created by admin (role:admin)")]
    [HttpGet("admin/tests"), Authorize(Roles = "Administrator")]
    public async Task<ActionResult<List<TestInformation>>> GetTestsByAdmin()
    {
        var getTestsByAdminQuery = new GetTestsByAdminQuery()
        {
            AdminId = LoginId!.Value
        };

        var tests = await Mediator.Send(getTestsByAdminQuery);

        if (tests.Count == 0)
        {
            return NoContent();
        }

        return tests;
    }

    [SwaggerOperation(Summary = "Get all tests assigned for user (role:user)")]
    [HttpGet("user/tests"), Authorize(Roles = "User")]
    public async Task<ActionResult<List<TestInformation>>> GetTestsByUser([FromQuery] string filter)
    {
        var parseResult = Enum.TryParse(filter, true, out UserTestsFilter filterValue);
        if (!parseResult)
        {
            return BadRequest(new ErrorResult(HttpStatusCode.BadRequest, "Invalid filter value"));
        }

        var getTestsByUserQuery = new GetTestsByUserQuery()
        {
            UserId = LoginId!.Value,
            Filter = filterValue
        };

        var tests = await Mediator.Send(getTestsByUserQuery);

        if (tests.Count == 0)
        {
            return NoContent();
        }

        return tests;
    }

    [SwaggerOperation(Summary = "Get test by id (role:user,admin)")]
    [HttpGet("tests/{testId:guid}"), Authorize(Roles = "User, Administrator")]
    public async Task<ActionResult<Domain.Test?>> GetTestById([FromRoute] Guid testId)
    {
        var getTestByIdQuery = new GetTestByIdQuery()
        {
            TestId = testId
        };

        var test = await Mediator.Send(getTestByIdQuery);

        if (test is null)
        {
            return NotFound(new ErrorResult(HttpStatusCode.NotFound, $"Test with {testId} doesn't exist"));
        }

        return test;
    }

    [SwaggerOperation(Summary = "Delete test (role:admin)")]
    [HttpDelete("tests/{testId:guid}"), Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteTest([FromRoute] Guid testId)
    {
        var deleteTestCommand = new DeleteTestCommand()
        {
            AuthorId = LoginId!.Value,
            TestId = testId
        };

        await Mediator.Send(deleteTestCommand);

        return Ok();
    }

    [SwaggerOperation(Summary = "Get users assigned to the test (role:admin)")]
    [HttpGet("tests/{testId:guid}/users"), Authorize(Roles = "Administrator")]
    public async Task<ActionResult<List<UserOnTest>>> GetUsersOnTest([FromRoute] Guid testId)
    {
        var getUsersOnTestQuery = new GetUsersOnTestQuery()
        {
            TestId = testId
        };

        var users = await Mediator.Send(getUsersOnTestQuery);

        if (users.Count == 0)
        {
            return NoContent();
        }

        return users;
    }

    [SwaggerOperation(Summary = "Assign user to existing test (role:admin)")]
    [HttpPost("tests/{testId:guid}/users/{userId:guid}"), Authorize(Roles = "Administrator")]
    public async Task<IActionResult> AssignUserToTest([FromRoute] Guid testId, [FromRoute] Guid userId)
    {
        var createTestAssignmentCommand = new CreateTestAssignmentCommand()
        {
            TestId = testId,
            UserId = userId
        };

        await Mediator.Send(createTestAssignmentCommand);
        return Ok();
    }

    [SwaggerOperation(Summary = "Assign users from group to existing test (role:admin)")]
    [HttpPost("tests/{testId:guid}/groups/{groupId:guid}"), Authorize(Roles = "Administrator")]
    public async Task<IActionResult> AssignUserGroupToTest([FromRoute] Guid testId, [FromRoute] Guid groupId)
    {
        var createTestAssignmentByGroupCommand = new CreateTestAssignmentByGroupCommand()
        {
            TestId = testId,
            UserGroupId = groupId
        };

        await Mediator.Send(createTestAssignmentByGroupCommand);

        return Ok();
    }

    [SwaggerOperation(Summary = "Delete test assignment (role:admin)")]
    [HttpDelete("tests/{testId:guid}/users/{userId:guid}"), Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteTestAssignment([FromRoute] Guid testId, [FromRoute] Guid userId)
    {
        var deleteTestAssignmentCommand = new DeleteTestAssignmentCommand()
        {
            TestId = testId,
            UserId = userId
        };

        await Mediator.Send(deleteTestAssignmentCommand);

        return Ok();
    }

    [SwaggerOperation(Summary = "Submit test results (role:user)")]
    [HttpPost("tests/{testId:guid}/results"), Authorize(Roles = "User")]
    public async Task<IActionResult> SubmitTestResult([FromBody] TestCompletionDto testCompletionDto)
    {
        var submitTestResultCommand = new SubmitTestResultCommand()
        {
            UserId = LoginId!.Value,
            TestId = testCompletionDto.TestId,
            TestCompletion = new TestCompletion()
            {
                Score = testCompletionDto.Score,
                CompletionTime = testCompletionDto.CompletionTime,
                Answers = testCompletionDto.History
                .SelectMany(x => x.Answers.Select(text => new AnswerHistory()
                {
                    QuestionId = x.QuestionId,
                    AnswerText = text
                })).ToArray()
            }
        };

        await Mediator.Send(submitTestResultCommand);

        return Ok();
    }

    [SwaggerOperation(Summary = "Get test results (role:user,admin)")]
    [HttpGet("tests/{testId:guid}/results"), Authorize(Roles = "User, Administrator")]
    public async Task<ActionResult<TestCompletionDto>> GetTestResult([FromRoute] Guid testId, [FromQuery] Guid userId = default)
    {
        var getTestResultQuery = new GetTestResultQuery()
        {
            TestId = testId
        };

        if (LoginRole!.Value == LoginType.Administrator)
        {
            getTestResultQuery.UserId = userId;
        }
        else if (LoginRole!.Value == LoginType.User)
        {
            getTestResultQuery.UserId = LoginId!.Value;
        }

        var testCompletion = await Mediator.Send(getTestResultQuery);

        return new TestCompletionDto()
        {
            TestId = testId,
            Score = testCompletion.Score,
            CompletionTime = testCompletion.CompletionTime,
            History = testCompletion.Answers
            .GroupBy(x => x.QuestionId)
            .Select(x => new AnswerHistoryDto()
            {
                QuestionId = x.Key,
                Answers = x.Select(x => x.AnswerText).ToArray()
            }).ToArray()
        };
    }
}
