using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RmbClone.Library.Models
{
    public class FaqDBModel
    {
       
        public string Id { get; set; }
        [Required]
        public string QuestionBj { get; set; }
        [Required]
        public string AnswerBj { get; set; }
        [Required]
        public string QuestionEn { get; set; }
        [Required]
        public string AnswerEn { get; set; }

        public FaqDBModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
