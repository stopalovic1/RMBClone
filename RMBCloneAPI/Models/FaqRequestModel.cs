using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RmbCloneAPI.Models
{
    public class FaqRequestModel
    {
        [Required]
        public string QuestionBj { get; set; }
        [Required]
        public string AnswerBj { get; set; }
        [Required]
        public string QuestionEn { get; set; }
        [Required]
        public string AnswerEn { get; set; }
    }
}
