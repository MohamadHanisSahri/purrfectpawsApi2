using System;
using System.Collections.Generic;

namespace purrfectpawsApi2.Models;

public partial class TTransaction
{
    public int TransactionId { get; set; }

    public int CartId { get; set; }

    public int PaymentStatusId { get; set; }

    public int OrderMasterId { get; set; }

    public DateTime TransactionDate { get; set; }

    public decimal TransactionAmount { get; set; }
}
