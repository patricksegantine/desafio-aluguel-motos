using RentAMotto.Domain.DomainObjects.Enums;

namespace RentAMotto.Domain.Entities;

public class RentalContract : Entity
{
    public int VehicleId { get; private set; }
    public int DeliveryDriverId { get; private set; }
    public int RentalPlanId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime ExpectedEndDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    public decimal RentalAmount { get; private set; }
    public decimal FineAmount { get; private set; }
    public decimal TotalAmount => RentalAmount + FineAmount;
    public RentalStatusType Status { get; private set; }

    public Vehicle Vehicle { get; set; }
    public DeliveryDriver DeliveryDriver { get; set; }
    public RentalPlan RentalPlan { get; set; }

    public RentalContract()
    {
        if (Status == RentalStatusType.Open)
        {
            RentalAmount = CalculateAmount(DateTime.Now);
            FineAmount = CalculateFine(DateTime.Now);
        }
    }

    /// <summary>
    /// Cria um contrato de locação. O início da locação obrigatóriamente é o primeiro dia após a data de criação.
    /// </summary>
    /// <param name="vehicle"></param>
    /// <param name="deliveryDriver"></param>
    /// <param name="rentalPlan"></param>
    /// <param name="expectedEndDate"></param>
    /// <returns></returns>
    public static RentalContract Create(Vehicle vehicle, DeliveryDriver deliveryDriver, RentalPlan rentalPlan, DateTime expectedEndDate)
    {
        var createdDate = DateTime.Now;
        var startDate = createdDate.AddDays(1);

        return new RentalContract
        {
            VehicleId = vehicle.Id,
            DeliveryDriverId = deliveryDriver.Id,
            RentalPlanId = rentalPlan.Id,
            StartDate = startDate,
            ExpectedEndDate = expectedEndDate,
            EndDate = null,
            CreatedDate = createdDate,
            UpdatedDate = null,
        };
    }

    /// <summary>
    /// Encerra o contrato de locação
    /// </summary>
    public void Close()
    {
        EndDate = DateTime.Now;
        RentalAmount = CalculateAmount(EndDate.Value);
        FineAmount = CalculateFine(EndDate.Value);
        Status = RentalStatusType.Closed;
    }


    /// <summary>
    /// Calcula o valor total das diárias (amount) e o valor da multa (fine) baseado em uma data de referência 
    /// </summary>
    /// <param name="returnDate">data de referência para cálculo</param>
    /// <returns>Tupla com amount e fine</returns>
    public (decimal amount, decimal fine) CalculateAmountAndFineBalance(DateTime returnDate)
    {
        decimal amount = CalculateAmount(returnDate);
        decimal fine = CalculateFine(returnDate);

        return (amount, fine);
    }

    /// <summary>
    /// Calcula o valor total das diárias (amount)
    /// </summary>
    /// <param name="returnDate"></param>
    /// <returns>Amount</returns>
    private decimal CalculateAmount(DateTime returnDate)
    {
        var usedDays = (ExpectedEndDate.Date - StartDate.Date).Days;

        if (returnDate.Date < ExpectedEndDate.Date)
        {
            usedDays = (returnDate - StartDate.Date).Days;
        }

        return usedDays * RentalPlan.CostPerDay;
    }

    /// <summary>
    /// Calcula o valor da multa (fine)
    /// </summary>
    /// <param name="returnDate"></param>
    /// <returns>Fine</returns>
    private decimal CalculateFine(DateTime returnDate)
    {
        decimal fineAmount = 0;

        if (returnDate.Date < ExpectedEndDate.Date)
        {
            var notUsedDays = (ExpectedEndDate.Date - returnDate.Date).Days;
            fineAmount = notUsedDays * (RentalPlan.PercentageOfFineForReturnBeforeExpectedEndDatePerDay / 100);
        }
        else if (returnDate.Date > ExpectedEndDate.Date)
        {
            var addedDays = (returnDate.Date - ExpectedEndDate.Date).Days;
            fineAmount = addedDays * RentalPlan.AmountOfFineForReturnAfterExpectedEndDatePerDay;
        }

        return fineAmount;
    }
}
