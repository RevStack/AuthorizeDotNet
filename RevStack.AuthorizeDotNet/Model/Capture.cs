﻿using RevStack.Payment.Model;

namespace RevStack.AuthorizeDotNet.Model
{
    public class Capture : ICapture
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
    }
}
