﻿using System.Net.Http;
using SchoolSystem.Models;
using SchoolSystem.Repository;
using SchoolSystem.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net;
using SchoolSystem.WebApi.Helpers;

namespace SchoolSystem.WebApi.Controllers
{
    public class StudentController : ApiController
    {
        private readonly IRepository<Student> repository;

        public StudentController(IRepository<Student> repository)
        {
            this.repository = repository;
        }

        //public StudentController() 
        //{
        //    this.repository = new EfRepository<Student>(new SchoolContext());
        //}

        // GET api/student
        public IEnumerable<StudentModel> Get()
        {
            return this.repository.All().Select(StudentModel.FormStudent);
        }

        //GET api/student/5
        public StudentDetailedModel Get(int id)
        {
            return this.repository.All().Where(x => x.StudentId == id).Select(StudentDetailedModel.FormStudent).FirstOrDefault();
        }

        //GET api/student/5
        [HttpGet]
        public IQueryable<StudentDetailedModel> Get(string subject, decimal value)
        {
            var students = this.repository.All().Where(
                y => (
                    y.Marks
                    .Where(
                    x => x.Subject.ToLower() == subject.ToLower() 
                        && x.Value >= value).FirstOrDefault()) != null)
                .Select(StudentDetailedModel.FormStudent);

            return students;
        }

        public IQueryable<StudentDetailedModel> GetStudentBySchool(int schoolId)
        {
            return this.repository.All().Where(x => x.School.SchoolId == schoolId).Select(StudentDetailedModel.FormStudent);
        }

        // POST api/student
        public HttpResponseMessage Post([FromBody]Student student)
        {
            try
            {
                this.repository.Add(student);
                var response = Request.CreateResponse<Student>(HttpStatusCode.Created, student);
                var resourceLink = Url.Link("DefaultApi", new { id = student.StudentId });
                response.Headers.Location = new Uri(resourceLink);
                return response;
            }

            catch (Exception ex)
            {
                var response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                return response;
            }
        }

        // PUT api/student/5
        public HttpResponseMessage Put(int id, [FromBody]Student student)
        {
            if (this.repository.Get(id) != null)
            {
                this.repository.Update(id, student);
                return this.Request.CreateResponse(HttpStatusCode.Accepted);
            }
            else
            {
                var response = this.Request.CreateResponse(HttpStatusCode.BadRequest);
                response.ReasonPhrase = "The record you have try to change not found!";
                return response;
            }
        }

        // DELETE api/student/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                this.repository.DeleteChilds(id);
                this.repository.Delete(id);
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

            string successMessage = string.Format("Record ID {0} was deleted", id);

            return this.Request.CreateResponse(HttpStatusCode.Accepted, successMessage);
        }

    }
}