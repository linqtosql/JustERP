namespace JustERP.Core.User.Orders
{
    public static class LhzxExpertOrderExtensions
    {
        public static bool IsPayingOrder(this LhzxExpertOrder order)
        {
            return order.Status == (int)ExpertOrderStatus.Paying;
        }
        public static bool IsPayedOrder(this LhzxExpertOrder order)
        {
            return order.Status > (int)ExpertOrderStatus.Paying;
        }

        public static bool IsExpertOrder(this LhzxExpertOrder order, long expertId)
        {
            return order.ExpertId == expertId || order.ServerExpertId == expertId;
        }
    }
}
