﻿using Microsoft.AspNetCore.Mvc;

using HomeAloneBackend.Models;
using HomeAloneBackend.Services;

namespace HomeAloneBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmitController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;

        public SubmitController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        // POST: api/Submit
        [HttpPost]
        public void Post(ApiDataModel formData)
        {
            _fileUploadService.Save(formData);
        }
    }
}