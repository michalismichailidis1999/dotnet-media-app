﻿global using MediaApp.Application.Repositories.User;
global using MediaApp.Application.Services.UserService;
global using Microsoft.AspNetCore.Mvc;
global using MediaApp.Api.Registers;
global using MediaApp.Api.Extensions;
global using Microsoft.EntityFrameworkCore;
global using System.ComponentModel.DataAnnotations;
global using MediaApp.Api.Dtos.Request.UserDtos;
global using AutoMapper;
global using MediaApp.Domain.Aggregates.UserAggregates;
global using MediaApp.Infrastructure.PasswordHashers;
global using MediaApp.Api.Dtos.Response.UserDtos;
global using MediaApp.Api.Dtos.Response.ErrorDtos;
global using Microsoft.AspNetCore.Mvc.Filters;
global using MediaApp.Api.Filters;
global using Microsoft.AspNetCore.Mvc.ModelBinding;
global using MediaApp.Api.Filters.Models;
global using MediaApp.Common.Enums;
global using MediaApp.Infrastructure.Options;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using MediaApp.Infrastructure.Auth;
global using Microsoft.AspNetCore.Mvc.Versioning;
global using StackExchange.Redis;
global using MediaApp.Infrastructure.Cache;
global using MediaApp.Application.Services;
global using MediaApp.Api.Dtos.Request.PostDtos;
global using MediaApp.Domain.Aggregates.PostAggregates;
global using MediaApp.Application.Services.PostService;
global using MediaApp.Api.Dtos.Response.PostDtos;
global using MediaApp.Application.Repositories.Post;
global using Microsoft.AspNetCore.Authorization;
global using MediaApp.Api.Dtos.Request.CommentDtos;
global using MediaApp.Application.Services.CommentService;
global using MediaApp.Api.Dtos.Response.CommentDtos;
global using MediaApp.Application.Repositories.Comment;