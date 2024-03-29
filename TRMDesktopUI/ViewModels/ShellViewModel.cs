﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using TRMDesktopUI.EventModels;
using TRMDestopUI.Library.Api;
using TRMDestopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        IEventAggregator _events;
        SalesViewModel _salesVM;
        ILoggedInUserModel _user;
        private IAPIHelper _aPIHelper;
        

        public ShellViewModel(IEventAggregator events, ILoggedInUserModel user, IAPIHelper aPIHelper)

        {
            _events = events;
            _user = user;
            _aPIHelper = aPIHelper;
            
            _events.SubscribeOnPublishedThread(this);

            ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
        }

        public bool IsLoggedIn
        {
            get
            {
                bool output = false;
                if (string.IsNullOrWhiteSpace( _user.Token) == false)
                {
                    output = true;
                }
                return output;
            }
        }

        public async Task LogOut()
        {
            _user.ResetUserModel();
            _aPIHelper.LogOffUser();
            await ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public void ExitApplication()
        {
            TryCloseAsync();
        }


        public async Task UserManagement()
        {
            await ActivateItemAsync(IoC.Get<UserDisplayViewModel>(), new CancellationToken());
        }
     

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(IoC.Get<SalesViewModel>(), cancellationToken);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
    }
}
