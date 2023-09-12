using System;
using System.Collections.Generic;

namespace purrfectpawsApi2.Models;

public partial class MPaymentStatus
{
    public int PaymentStatusId { get; set; }

    public string PaymentStatus { get; set; } = null!;
}
