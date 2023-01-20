using AppApi.Entities;
using AppApi.Helpers;

namespace AppApi.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<UserBill> ApplyUserParameters(this IQueryable<UserBill> query, UserParams userParams)
        {
            query = userParams.Type switch
            {
                "electricity" => query.Where(bill => bill.Type == BillType.Electricity),
                "naturalgas" => query.Where(bill => bill.Type == BillType.NaturalGas),
                "both" => query.Where(bill => bill.Type == BillType.Both),
                _ => query
            };

            query = userParams.Status switch
            {
                "approved" => query.Where(bill => bill.Status == Status.Approved),
                "rejected" => query.Where(bill => bill.Status == Status.Rejected),
                "pending" => query.Where(bill => bill.Status == Status.Pending),
                _ => query
            };

            query = userParams.State switch
            {
                "saved" => query.Where(bill => bill.State == State.Saved),
                _ => query.Where(bill => bill.State == State.Submitted)
            };

            var minDateExpression = 12*(userParams.MinYear - 2022) + userParams.MinMonth;
            query = query.Where(bill => (12*(bill.Year - 2022) + (int)bill.Month) >= minDateExpression);  

            var maxDateExpression = 12*(userParams.MaxYear - 2022) + userParams.MaxMonth;
            query = query.Where(bill => (12*(bill.Year - 2022) + (int)bill.Month) <= maxDateExpression);            

            query = userParams.OrderBy switch
            {
                "dateoldest" => query.OrderBy(bill => bill.Year).ThenBy(bill => bill.Month).ThenBy(bill => bill.Type),
                _ => query.OrderByDescending(bill => bill.Year).ThenByDescending(bill => bill.Month).ThenBy(bill => bill.Type)
            };

            return query;
        }
    }
}