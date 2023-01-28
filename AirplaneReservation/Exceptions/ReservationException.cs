using System;

namespace AirplaneReservation.Exceptions
{
    public class ReservationException : Exception
    {        
        public int DeclaredAmount { get; set; }
        public int SelectedAmount { get; set; }
        public ReservationException(int declaredAmount, int selectedAmount) : base($"Wybrano niezgodną ilość miejsc ({selectedAmount}) względem uprzednio zadeklarowanej ({declaredAmount}).")
        {
            DeclaredAmount = declaredAmount;
            SelectedAmount = selectedAmount;
        }
    }
}
