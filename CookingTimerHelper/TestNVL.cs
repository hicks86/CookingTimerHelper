using Csla;
using System;

namespace CookingTimerHelper
{
    [Serializable]
    public class TestNVL : NameValueListBase<int, string>
    {
        public TestNVL()
        {
            
        }

        #region Data Access

        [Create]
        private void Create()
        {
            IsReadOnly = false;
            Add(new NameValuePair(1, "Test1"));
            Add(new NameValuePair(2, "Test2"));
            Add(new NameValuePair(3, "Test3"));
            Add(new NameValuePair(4, "Test4"));
            IsReadOnly = true;
        }

        #endregion
    }
}
