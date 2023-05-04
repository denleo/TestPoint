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
using TestPoint.Application.Tests.Queries.GetTestById;
using TestPoint.Application.Tests.Queries.GetTestsByAdmin;
using TestPoint.Domain;
using TestPoint.WebAPI.Middlewares.CustomExceptionHandler;
using TestPoint.WebAPI.Models.Test;

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

    [SwaggerOperation(Summary = "Get all tests (role:admin)")]
    [HttpGet("tests"), Authorize(Roles = "Administrator")]
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

    [SwaggerOperation(Summary = "Get test by id (role:admin)")]
    [HttpGet("tests/{testId:guid}"), Authorize(Roles = "Administrator")]
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

    [SwaggerOperation(Summary = "Assign user to existing test (role:admin)")]
    [HttpPost("tests/{testId:guid}/users/{userId:guid}"), Authorize(Roles = "Administrator")]
    public async Task<ActionResult<TestAssignment>> AssignUserToTest([FromRoute] Guid testId, [FromRoute] Guid userId)
    {
        var createTestAssignmentCommand = new CreateTestAssignmentCommand()
        {
            TestId = testId,
            UserId = userId
        };

        var testAssignment = await Mediator.Send(createTestAssignmentCommand);
        return testAssignment;
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
}
