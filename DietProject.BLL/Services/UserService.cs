﻿using DietProject.DAL.Repositories;
using DietProject.Model.Entities;
using DietProject.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietProject.BLL.Services
{
    public class UserService
    {
        UserRepository userRepository;
        public UserService()
        {
            userRepository= new UserRepository();
        }
        public bool Insert(User user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName) || string.IsNullOrWhiteSpace(user.Email))
            {
                throw new Exception("Eksik alan doldurdunuz.");
            }
            Password firstPassword = user.Passwords.FirstOrDefault();
            if (firstPassword == null)
            {
                throw new Exception("Şifre giriniz");
            }


            user.CreationDate= DateTime.Now;
            user.Passwords.FirstOrDefault().CreationDate = DateTime.Now;
            user.IsActive = false;
            user.UserType = EnumUser.Standart;
            return userRepository.Insert(user);
        }
        public User CheckLogin(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password)) throw new Exception("UserName ve/veya password eksik");



            return userRepository.CheckLogin(userName, password);
        }
       public List<User> GetPassiveUsers()
        {
            return userRepository.GetPassiveUsers();
        }

        void CheckUserId(User user)
        {
            if (user.ID <= 0) throw new Exception("User id boş olamaz");
        }

        public void ActivateUser(int UserID)
        {
            User user = userRepository.GetUserById(UserID);
            CheckUserId(user);
            userRepository.ActivateUser(user);
        }
     
        public string FindEmailIfExists(string email)
        {
            return userRepository.FindEmailIfExists(email);
        }

    }
}
