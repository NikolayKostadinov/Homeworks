﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using FileUpload.Data;
using FileUpload.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FileUpload.Models.ViewModels
{
    public class AccountViewModel
    {
        private IUowData db;
        private ICollection<UserRoleIntPk> userInRoles = new HashSet<UserRoleIntPk>();

        public AccountViewModel() : this(DependencyResolver.Current.GetService(typeof(IUowData)) as IUowData)
        {
        }

        //Dependanci injector constructor
        public AccountViewModel(IUowData db) 
        {
            this.db = db;
        }
       
        public static Expression<Func<ApplicationUser, AccountViewModel>> ToAccountViewModel
        {
            get
            {
                return (a) => new AccountViewModel()
                {
                    Id = a.Id,
                    UserName = a.UserName,
                    Email = a.Email,
                    PhoneNumber = a.PhoneNumber,
                    UserInRole = a.Roles
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public ICollection<UserRoleIntPk> UserInRole 
        {
            get 
            {
                return this.userInRoles;
            }
            set 
            {
                this.userInRoles = value;
            }
        }

        public ICollection<RoleViewModel> Roles
        {
            get
            {
                if (this.userInRoles == null)
                {
                    return null;
                }
                else
                {
                    HashSet<RoleViewModel> usersRoles = new HashSet<RoleViewModel>();

                    foreach (var role in this.userInRoles)
                    {
                        var currentRole = db.Roles.GetById(role.RoleId);
                        usersRoles.Add(new RoleViewModel() { Id = currentRole.Id, Name = currentRole.Name });
                    }

                   return usersRoles;
                }
            }
            set 
            {
                if (value == null)
                {
                    return;
                }
                else
                {
                    this.userInRoles.Clear();

                    foreach (var role in value) 
                    {
                        this.UserInRole.Add(new UserRoleIntPk() { UserId = this.Id, RoleId = role.Id });
                    }
                }
            }
        }
    }
}