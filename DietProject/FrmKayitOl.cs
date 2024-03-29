﻿using DietProject.BLL.Services;
using DietProject.DAL;
using DietProject.Model.Entities;
using DietProject.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DietProject
{
    public partial class FrmKayitOl : Form
    {
        UserService userService;
        CaloriesDBContext db;
        public FrmKayitOl()
        {
            db = new CaloriesDBContext();
            userService= new UserService();
            InitializeComponent();
        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {




            
            string eMail = txtID.Text.Trim();
            try
            {

                var mail = userService.FindEmailIfExists(eMail);
                MessageBox.Show("Bu E-Mail kullanımda");
                return;
            }
            catch (Exception)
            {

                
               
            }
           

            if (txtPassword.Text != txtPasswordRepeat.Text)
            {
                MessageBox.Show("Şifreler birbirini tutmuyor");
                return;
            }
            try
            {
                User user = new User()
                {
                    FirstName = txtUserAd.Text,
                    LastName = txtUserSoyad.Text,
                    Email = txtID.Text,
                    Gender = rbMale.Checked ? EnumGender.Male : EnumGender.Female,
                    UserType = EnumUser.Standart,
                    Age=(int)nudYas.Value,
                    Weight= (int)nudKilo.Value,
                    Height= (int)nudHeight.Value,

                };
                user.Passwords.Add(new Password()
                {
                    UserPassword = Encode(txtPassword.Text)
                });
                bool check = userService.Insert(user);
                MessageBox.Show(check ? "Kullanıcı eklendi" : "Kullanıcı eklenemedi");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
           
        }
        public string Encode(string password)
        {
            try
            {
                string ourText = password;
                string Return = null;
                string _key = "abcdefgh";
                string privatekey = "hgfedcba";
                byte[] privatekeyByte = { };
                privatekeyByte = Encoding.UTF8.GetBytes(privatekey);
                byte[] _keybyte = { };
                _keybyte = Encoding.UTF8.GetBytes(_key);
                byte[] inputtextbyteArray = System.Text.Encoding.UTF8.GetBytes(ourText);
                using (DESCryptoServiceProvider dsp = new DESCryptoServiceProvider())
                {
                    var memstr = new MemoryStream();
                    var crystr = new CryptoStream(memstr, dsp.CreateEncryptor(_keybyte, privatekeyByte), CryptoStreamMode.Write);
                    crystr.Write(inputtextbyteArray, 0, inputtextbyteArray.Length);
                    crystr.FlushFinalBlock();
                    return Convert.ToBase64String(memstr.ToArray());
                }
                return Return;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
