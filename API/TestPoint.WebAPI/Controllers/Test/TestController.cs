using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
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
            CompletionTime = newTest.CompletionTime,
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
    public async Task<ActionResult<List<Domain.Test>>> GetTestsByAdmin()
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
    [HttpGet("tests/{id:guid}"), Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Domain.Test?>> GetTestById([FromRoute] Guid id)
    {
        var getTestByIdQuery = new GetTestByIdQuery()
        {
            TestId = id
        };

        var test = await Mediator.Send(getTestByIdQuery);

        if (test is null)
        {
            return NotFound(new ErrorResult(HttpStatusCode.NotFound, $"Test with {id} doesn't exist"));
        }

        return test;
    }

    [SwaggerOperation(Summary = "Delete test (role:admin)")]
    [HttpDelete("tests/{id:guid}"), Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteTest([FromRoute] Guid id)
    {
        var deleteTestCommand = new DeleteTestCommand()
        {
            AuthorId = LoginId!.Value,
            TestId = id
        };

        await Mediator.Send(deleteTestCommand);

        return Ok();
    }
}
