using Backend.DTO.Slot;
using Backend.Models;

namespace Backend.Services
{
    public class SlotService
    {
        private readonly BmvContext _bmvContext = new BmvContext();
        public List<Slot> GetAllSlots()
        {
            return _bmvContext.Slots.ToList();
        }
        public Slot GetSlotById(int id)
        {
            return _bmvContext.Slots.Find(id);
        }
        public bool AddSlot(Slot s)
        {
            try
            {
                _bmvContext.Slots.Add(s);
                _bmvContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Slot UpdateSlot(int id,PutSlotDTO s)
        {
            var us = _bmvContext.Slots.Find(id);
            if (us == null)
            {
                return null;
            }
            Console.WriteLine(s.WeekdayPrice);
            us.WeekendPrice = s.WeekendPrice == null? us.WeekendPrice : (double)s.WeekendPrice;
            us.WeekdayPrice = s.WeekdayPrice == null ? us.WeekdayPrice : (double)s.WeekdayPrice;
            us.IsBlocked = s.IsBlocked == null ? us.IsBlocked : (bool)s.IsBlocked;
            _bmvContext.SaveChanges();
            return us;
        }
        public bool DeleteSlot(int id)
        {
            var ds = _bmvContext.Slots.Find(id);
            if (ds != null)
            {
                _bmvContext.Slots.Remove(ds);
                _bmvContext.SaveChanges();
                return true;
            }
            return false;
        }
      
    }
}
