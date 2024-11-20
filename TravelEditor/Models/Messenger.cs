using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEditor.Models
{
    public class Messenger
    {
        public static event Action DataChanged;

        public static void NotifyDataChanged()
        {
            DataChanged?.Invoke();
        }
    }
}
