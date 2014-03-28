﻿namespace SchoolSystem.Repository.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using SchoolSystem.Data;
    using SchoolSystem.Models;

    public class StudentDetailedModel
    {
        public static Expression<Func<Student, StudentDetailedModel>> FormStudent
        {
            get 
            { 
                return x => new StudentDetailedModel { 
                    StudentId = x.StudentId, 
                    FirstName = x.FirstName, 
                    LastName = x.LastName, 
                    Age = x.Age, 
                    Marks = x.Marks.AsQueryable().Select(MarkModel.FromMark).ToList<MarkModel>() 
                }; 
            }
        }

        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public School School { get; private set; } 

        public virtual ICollection<MarkModel> Marks { get; private set; }
    }
}