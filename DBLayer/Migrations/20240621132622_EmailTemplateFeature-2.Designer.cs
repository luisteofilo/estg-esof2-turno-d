﻿// <auto-generated />
using System;
using ESOF.WebApp.DBLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ESOF.WebApp.DBLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240621132622_EmailTemplateFeature-2")]
    partial class EmailTemplateFeature2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.Education", b =>
                {
                    b.Property<Guid>("EducationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("EndDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uuid");

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StartDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("EducationId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.Emails.EmailTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EmailTemplates");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.Experience", b =>
                {
                    b.Property<Guid>("ExperienceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("ExperienceId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Experiences");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.Permission", b =>
                {
                    b.Property<Guid>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PermissionId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.Profile", b =>
                {
                    b.Property<Guid>("ProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UrlBackground")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UrlProfile")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("ProfileId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.ProfileSkill", b =>
                {
                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SkillId")
                        .HasColumnType("uuid");

                    b.HasKey("ProfileId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("ProfileSkills");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.RolePermission", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uuid");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.Skill", b =>
                {
                    b.Property<Guid>("SkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SkillId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.UserRole", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.Education", b =>
                {
                    b.HasOne("ESOF.WebApp.DBLayer.Entities.Profile", "Profile")
                        .WithMany("Educations")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.Experience", b =>
                {
                    b.HasOne("ESOF.WebApp.DBLayer.Entities.Profile", "Profile")
                        .WithMany("Experiences")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.Profile", b =>
                {
                    b.HasOne("ESOF.WebApp.DBLayer.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("ESOF.WebApp.DBLayer.Entities.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.ProfileSkill", b =>
                {
                    b.HasOne("ESOF.WebApp.DBLayer.Entities.Profile", "Profile")
                        .WithMany("ProfileSkills")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESOF.WebApp.DBLayer.Entities.Skill", "Skill")
                        .WithMany("ProfileSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.RolePermission", b =>
                {
                    b.HasOne("ESOF.WebApp.DBLayer.Entities.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESOF.WebApp.DBLayer.Entities.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.UserRole", b =>
                {
                    b.HasOne("ESOF.WebApp.DBLayer.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESOF.WebApp.DBLayer.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.Profile", b =>
                {
                    b.Navigation("Educations");

                    b.Navigation("Experiences");

                    b.Navigation("ProfileSkills");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.Skill", b =>
                {
                    b.Navigation("ProfileSkills");
                });

            modelBuilder.Entity("ESOF.WebApp.DBLayer.Entities.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
