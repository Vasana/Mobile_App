using Agent_App.Helpers;
using Agent_App.Models;
using Agent_App.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace Agent_App.ViewModels
{
    public class NotificsViewModel: INotifyPropertyChanged
    {
        private const int PageSize = 10;
        ApiServices _apiServices = new ApiServices();
        public Notification _previousNotif;

        public InfiniteScrollCollection<Notification> NotifCollection
        {
            get { return _notifics; }
            set
            {
                _notifics = value;
                OnPropertyChanged();
            }
        }
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        private bool _isBusy;

        public bool IsBusy2
        {
            get { return _isBusy2; }
            set
            {
                _isBusy2 = value;
                OnPropertyChanged();
            }
        }
        public bool _isBusy2;

        public InfiniteScrollCollection<Notification> _notifics;

        public object SelectedItem { get; set; }

        public NotificsViewModel()
        {
            DownloadNotifsAsync();
        }

        public async Task DownloadNotifsAsync()
        {
            NotifCollection = new InfiniteScrollCollection<Notification>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = NotifCollection.Count / PageSize;

                    var items = await _apiServices.GetNotificationsAsync(Settings.AccessToken, page, PageSize);

                    IsBusy = false;

                    // return the items that need to be added
                    return items;
                },
                OnCanLoadMore = () =>
                {
                    return NotifCollection.Count < _apiServices.notifCount;
                }
            };
            _previousNotif = null;
            IsBusy2 = true;
            var items2 = await _apiServices.GetNotificationsAsync(accessToken: Settings.AccessToken, pageIndex: 0, pageSize: PageSize);            
            IsBusy2 = false;
            NotifCollection.AddRange(items2);
            //PoliciesCollection = new InfiniteScrollCollection<CustPolicy>(items);
        }

        public async Task ClearNotifAsync()
        {
            /* IsBusy2 = true;
             bool ret = await _apiServices.ClearNotifAsync_1(Settings.AccessToken, notif);            
             if (ret)
             {
                 NotifCollection.Remove(notif);
             }
             IsBusy2 = false;
             return ret;
             */
            IEnumerable<Notification> delNotifList = NotifCollection.Where(p => p.IsMarked == true);
            if (delNotifList != null)
            {                
                List<Notification> delNotifs = delNotifList.ToList();
                _previousNotif = null;
                IsBusy2 = true;
                var items = await _apiServices.ClearNotifAsync(accessToken: Settings.AccessToken, notifList: delNotifs, pageIndex: 0, pageSize: PageSize);
                IsBusy2 = false;
                NotifCollection.Clear();
                NotifCollection.AddRange(items);
            }           

        }

        public Command DeleteNotifClicked
        {
            get
            {
                return new Command(async (e) =>
                {
                    var itm = e as Notification;
                    var rst = await App.Current.MainPage.DisplayAlert("Notification", "Are you sure you want to Delete ???", "Yes", "No");
                    if (rst)
                    {
                        IsBusy2 = true;
                        bool ret = await _apiServices.ClearNotifAsync_1(Settings.AccessToken, itm);
                        IsBusy2 = false;
                        if (ret)
                        {
                            NotifCollection.Remove(itm);
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Notification", "Sorry!!! Due to some issue record can't be Deleted.", "Ok");
                        }                 
                        
                    }
                });
            }
        }




        public void HideOrShowNotif(Notification notif)
        {
            if (_previousNotif == notif)
            {
                //clicking twice on same item will hide it
                notif.IsSelected = !notif.IsSelected;
                UpdatePolicies(notif);

            }
            else
            {
                if (_previousNotif != null)
                {
                    //hide previous selected item
                    _previousNotif.IsSelected = false;
                    UpdatePolicies(_previousNotif);
                }
                //show selected item
                notif.IsSelected = true;
                UpdatePolicies(notif);
            }

            _previousNotif = notif;
        }

        private void UpdatePolicies(Notification notif)
        {
            var index = NotifCollection.IndexOf(notif);
            NotifCollection.Remove(notif);
            NotifCollection.Insert(index, notif);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    
}
