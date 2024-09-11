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
        public bool UpdateSlot(int id,Slot s)
        {
            var us = _bmvContext.Slots.Find(id);
            if (us != null)
            {
                us.VenueId = s.VenueId;
                us.Start = s.Start;
                us.End = s.End;
                _bmvContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
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
