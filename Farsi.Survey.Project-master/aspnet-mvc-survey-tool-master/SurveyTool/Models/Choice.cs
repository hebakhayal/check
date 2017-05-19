using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyTool.Models
{
    public class Choice
    {
        [Key]
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Body { get; set; }
        public Question Question { get; set; }
    }
}