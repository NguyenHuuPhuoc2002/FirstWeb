﻿using FirstWeb.Models;
using FirstWeb.Repositories;
using FirstWeb.Views;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace FirstWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = new TimeSpan(0,15,0);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Đăng ký EmployeeRepository với Dependency Injection container
            builder.Services.AddSingleton<IStudentRepository<Student>, StudentRepository>();
            builder.Services.AddSingleton<IMajorRepository<Major>, MajorRepository>();
            builder.Services.AddScoped<IHistoryRepository<Student>, HistoryRepository>();
            builder.Services.AddScoped<ILoginRepository<Login>, LoginRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=DangNhap}/{action=DangNhapView}/{id?}");

            app.Run();
        }

    }
}
