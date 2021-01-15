using System;
using System.Linq;
using LearningPlatform.Data;
using LearningPlatform.Models.AnswerModels;
using LearningPlatform.Models.AssignmentModels;
using LearningPlatform.Services;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AssignmentController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult ShowAssignment(int moduleId, int courseId, int assignmentId)
        {
            var model = CourseService.ComposeCourseModel(_db, courseId, moduleId, assignmentId);
            ViewBag.Timestamps = model.Assignment.Questions.Select(q => q.CreationDate).ToList();
            ViewBag.Timestamps.AddRange(model.Assignment.Thoughts.Select(t => t.CreationDate).ToList());
            ViewBag.Timestamps.Sort();

            var studentId = StudentService.LoggedInStudent.Id;

            if (StudentService.IsAdmin) return View("ComposeAssignment", model);
            if (_db.AnswerAssignments.FirstOrDefault(a => a.AssignmentId == assignmentId && a.StudentId == studentId) ==
                null)
            {
                ViewBag.FirstAttempt = true;
                ViewBag.NoMoreAttempts = false;
            }
            else
            {
                ViewBag.FirstAttempt = false;
                var answer =
                    _db.AnswerAssignments.FirstOrDefault(
                        a => a.AssignmentId == assignmentId && a.StudentId == studentId);

                ViewBag.Grade = 0;
                ViewBag.NoMoreAttempts = model.Assignment.MaxTrials == answer.AttemptsTaken;
                model.AnswerAssignment = answer;
                answer.AnswerQuestionOptions =
                    _db.AnswerQuestionOptions.Where(opt => opt.AnswerAssignmentId == answer.Id).ToList();
                answer.AnswerThoughts = _db.AnswerThoughts.Where(t => t.AnswerAssignmentId == answer.Id).ToList();
            }

            return View("ViewAssignemnt", model);
        }

        public IActionResult CreateAssignment(int moduleId, int courseId)
        {
            var model = CourseService.ComposeCourseModel(_db, courseId, moduleId);
            return View("CreateAssignment", model);
        }

        [HttpPost]
        public IActionResult CreateAssignment(StudyCourseViewModel model, int courseId, int moduleId)
        {
            model.Assignment.ModuleId = moduleId;
            _db.Assignments.Add(model.Assignment);
            _db.SaveChanges();
            CourseService.FillModuleData(_db, model, moduleId);
            AssignmentService.ResolveOrders(_db, model, -1);
            return RedirectToAction("Study", "Course", new {courseId});
        }

        public IActionResult EditAssignment(int courseId, int moduleId, int assignmentId)
        {
            var model = CourseService.ComposeCourseModel(_db, courseId, moduleId);
            model.Assignment = _db.Assignments.Find(assignmentId);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditAssignment(StudyCourseViewModel model, int courseId, int moduleId, int oldOrder)
        {
            _db.Assignments.Update(model.Assignment);
            _db.SaveChanges();
            CourseService.FillModuleData(_db, model, moduleId);
            AssignmentService.ResolveOrders(_db, model, oldOrder);
            return RedirectToAction("Study", "Course", new {courseId});
        }

        public IActionResult DeleteAssignment(StudyCourseViewModel model, int assignmentId, int courseId)
        {
            var assignment = _db.Assignments.Find(assignmentId);
            _db.Assignments.Remove(assignment);
            _db.SaveChanges();
            CourseService.FillModuleData(_db, model, assignment.ModuleId);
            CourseService.ResolveOrdersAfterDelete(_db, model, assignment.Order);
            return RedirectToAction("Study", "Course", new {courseId});
        }

        public void ActionSaveChanges(StudyCourseViewModel model, int assignmentId)
        {
            var assignment = _db.Assignments.Find(assignmentId);
            assignment.Questions = _db.Questions.Where(q => q.AssignmentId == assignmentId)
                .Include(q => q.QuestionOptions).ToList();
            assignment.Thoughts = _db.Thoughts.Where(t => t.AssignmentId == assignmentId).ToList();
            model.Assignment = assignment;

            foreach (var q in model.Assignment.Questions)
            {
                foreach (var opt in q.QuestionOptions)
                {
                    opt.Text = Request.Form["questionOptionTextId" + opt.Id];
                    if (q.IsMultipleChoice)
                        opt.Correct = Request.Form["questionOptionCorrectId" + opt.Id] == "on";
                    else opt.Correct = int.Parse(Request.Form["questionOptionCorrectQId" + q.Id]) == opt.Id;
                    _db.QuestionOptions.Update(opt);
                }

                q.Name = Request.Form["questionNameId" + q.Id];
                q.MaxGrade = int.Parse(Request.Form["questionMaxGradeId" + q.Id]);
                _db.Questions.Update(q);
            }

            foreach (var t in model.Assignment.Thoughts)
            {
                t.Name = Request.Form["thoughtNameId" + t.Id];
                t.Content = Request.Form["thoughtContentId" + t.Id];
                t.MaxGrade = int.Parse(Request.Form["thoughtMaxGradeId" + t.Id]);
                _db.Thoughts.Update(t);
            }

            _db.SaveChanges();
        }

        public void ActionAddQuestion(bool isMultipleChoice, int assignmentId)
        {
            var question = new Question
            {
                Name = "New question",
                CreationDate = DateTime.Now,
                MaxGrade = 0,
                IsMultipleChoice = isMultipleChoice,
                AssignmentId = assignmentId
            };
            _db.Questions.Add(question);
            _db.SaveChanges();
        }

        public void ActionAddThought(int assignmentId)
        {
            var thought = new Thought
            {
                AssignmentId = assignmentId,
                Content = "Put your thought here",
                Name = "New thought",
                CreationDate = DateTime.Now
            };
            _db.Thoughts.Add(thought);
            _db.SaveChanges();
        }

        public void ActionDeleteQuestion(int questionId)
        {
            var question = _db.Questions.Find(questionId);
            _db.Questions.Remove(question);
            _db.SaveChanges();
        }

        public void ActionDeleteQuestionOption(int questionOptionId)
        {
            var questionOption = _db.QuestionOptions.Find(questionOptionId);
            _db.QuestionOptions.Remove(questionOption);
            _db.SaveChanges();
        }

        public void ActionDeleteThought(int thoughtId)
        {
            var thought = _db.Thoughts.Find(thoughtId);
            _db.Thoughts.Remove(thought);
            _db.SaveChanges();
        }

        public void ActionAddOption(int questionId)
        {
            var option = new QuestionOption {QuestionId = questionId, Text = "New option", Correct = false};
            _db.QuestionOptions.Add(option);
            _db.SaveChanges();
        }

        public IActionResult SaveChanges(StudyCourseViewModel model, int assignmentId, int questionId, int thoughtId,
            string actionName, int questionOptionId)
        {
            switch (actionName)
            {
                case "Save":
                    ActionSaveChanges(model, assignmentId);
                    break;
                case "addQuestion":
                    ActionSaveChanges(model, assignmentId);
                    ActionAddQuestion(false, assignmentId);
                    break;
                case "addMultQuestion":
                    ActionSaveChanges(model, assignmentId);
                    ActionAddQuestion(true, assignmentId);
                    break;
                case "addThought":
                    ActionSaveChanges(model, assignmentId);
                    ActionAddThought(assignmentId);
                    break;
                case "addOption":
                    ActionSaveChanges(model, assignmentId);
                    ActionAddOption(questionId);
                    break;

                case "deleteQuestion":
                    ActionDeleteQuestion(questionId);
                    ActionSaveChanges(model, assignmentId);
                    break;

                case "deleteQuestionOption":
                    ActionDeleteQuestionOption(questionOptionId);
                    ActionSaveChanges(model, assignmentId);
                    break;

                case "deleteThought":
                    ActionDeleteThought(thoughtId);
                    ActionSaveChanges(model, assignmentId);
                    break;
            }

            ViewBag.Timestamps = model.Assignment.Questions.Select(q => q.CreationDate).ToList();
            ViewBag.Timestamps.AddRange(model.Assignment.Thoughts.Select(t => t.CreationDate).ToList());
            ViewBag.Timestamps.Sort();
            model = CourseService.ComposeCourseModel(_db, model.Course.Id, model.Module.Id, assignmentId);
            return View("ComposeAssignment", model);
        }

        public IActionResult SaveResponse(StudyCourseViewModel model, int assignmentId, int courseId)
        {
            var studentId = StudentService.LoggedInStudent.Id;
            var firstAttempt =
                _db.AnswerAssignments.FirstOrDefault(a => a.AssignmentId == assignmentId && a.StudentId == studentId) ==
                null;
            var assignment = _db.Assignments.Find(assignmentId);
            assignment.Questions = _db.Questions.Where(q => q.AssignmentId == assignmentId)
                .Include(q => q.QuestionOptions).ToList();
            assignment.Thoughts = _db.Thoughts.Where(t => t.AssignmentId == assignmentId).ToList();
            model.Assignment = assignment;
            var response = _db.AnswerAssignments.FirstOrDefault(a => a.AssignmentId == assignmentId);
            if (response == null)
            {
                response = new AnswerAssignment
                {
                    StudentId = StudentService.LoggedInStudent.Id,
                    AssignmentId = assignmentId,
                    AttemptsTaken = 1,
                    LastTrial = DateTime.Now
                };
                _db.AnswerAssignments.Add(response);
                _db.SaveChanges();
                response = _db.AnswerAssignments.FirstOrDefault(a => a.AssignmentId == assignmentId);
            }
            else
            {
                response.LastTrial = DateTime.Now;
                response.AttemptsTaken++;
                _db.AnswerAssignments.Update(response);
                response.AnswerThoughts =
                    _db.AnswerThoughts.Where(at => at.AnswerAssignmentId == response.Id).ToList();
                response.AnswerQuestionOptions = _db.AnswerQuestionOptions
                    .Where(aqo => aqo.AnswerAssignmentId == response.Id).ToList();
            }

            foreach (var q in model.Assignment.Questions)
            {
                foreach (var opt in q.QuestionOptions)
                {
                    AnswerQuestionOption answerOption;
                    if (firstAttempt)
                    {
                        answerOption = new AnswerQuestionOption
                        {
                            AnswerAssignmentId = response.Id,
                            QuestionOptionId = opt.Id,
                        };
                    }
                    else
                        answerOption =
                            response.AnswerQuestionOptions.FirstOrDefault(o => o.QuestionOptionId == opt.Id);

                    answerOption ??= new AnswerQuestionOption
                    {
                        AnswerAssignmentId = response.Id,
                        QuestionOptionId = opt.Id,
                    };

                    if (q.IsMultipleChoice)
                    {
                        var value = Request.Form["questionOptionCorrectId" + opt.Id];
                        answerOption.Correct = value == "true" || value == "on";
                    }
                    else answerOption.Correct = int.Parse(Request.Form["questionOptionCorrectQId" + q.Id]) == opt.Id;

                    if (firstAttempt) _db.AnswerQuestionOptions.Add(answerOption);
                    else _db.AnswerQuestionOptions.Update(answerOption);
                }
            }

            foreach (var t in model.Assignment.Thoughts)
            {
                AnswerThought answerThought;
                if (firstAttempt)
                {
                    answerThought = new AnswerThought
                    {
                        ThoughtId = t.Id,
                        AnswerAssignmentId = response.Id
                    };
                }
                else answerThought = response.AnswerThoughts.FirstOrDefault(th => th.ThoughtId == t.Id);

                answerThought ??= new AnswerThought
                {
                    ThoughtId = t.Id,
                    AnswerAssignmentId = response.Id
                };

                answerThought.Content = Request.Form["thoughtContentId" + t.Id];
                if (firstAttempt) _db.AnswerThoughts.Add(answerThought);
                else _db.AnswerThoughts.Update(answerThought);
            }

            _db.SaveChanges();
            return RedirectToAction("Study", "Course", new {courseId});
        }
    }
}