using System;
using System.Collections.Generic;
using System.Text;

namespace AppGemini.Models
{
    public class Chat
    {
        public string Role { get; set; } = "user"; // "user" o "model"
        public string Text { get; set; } = "";
    }
}
