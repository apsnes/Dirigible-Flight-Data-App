using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightAppFrontend.Shared.Services
{
    public class JsInteropService : IJsInteropService
    {
        private readonly IJSRuntime _jsRuntime;

        public JsInteropService(IJSRuntime jsruntime)
        {
            _jsRuntime = jsruntime;
        }
        public async Task<string> GetItem(string key)
        {
            return await _jsRuntime.InvokeAsync<string>("localStorageFunctions.getItem", key);
        }

        public async Task SetItem(string key, string value)
        {
            await _jsRuntime.InvokeVoidAsync("localStorageFunctions.setItem", key, value);
        }
        public async void CreateAlert(string message)
        {
            await _jsRuntime.InvokeVoidAsync("alertMessage", message);
        }
        
        public async Task RemoveItem(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", key);
        }
    }
}
