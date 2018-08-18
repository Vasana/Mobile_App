using System;
using System.Collections.Generic;
using System.Text;
using Agent_App.Models;
using Agent_App.Services;
using Agent_App.Helpers;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Xamarin.Forms.Extended;

namespace Agent_App.ViewModels
{
    class BranchCntViewModel: INotifyPropertyChanged
    {
        public InfiniteScrollCollection<BranchContact> BranchesList
        {
            get { return _branches; }
            set
            {
                _branches = value;
                OnPropertyChanged();
            }
        }

        public InfiniteScrollCollection<BranchContact> _branches;

        public BranchContact _previousContact;

        ApiServices _apiServices = new ApiServices();

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

        public BranchCntViewModel()
        {
            getBranchesListAsync();
        }

        public async Task getBranchesListAsync()
        {
            BranchesList = new InfiniteScrollCollection<BranchContact>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    //var page = BranchesList.Count / PageSize;

                    var items2 = await _apiServices.GetBranchContactsAsync(Settings.AccessToken);
                    IsBusy = false;

                    // return the items that need to be added
                    return items2;
                },
                OnCanLoadMore = () =>
                {
                    return false;
                }
            };
            _previousContact = null;
            IsBusy = true;
            var items = await _apiServices.GetBranchContactsAsync(Settings.AccessToken);            
            IsBusy = false;
            BranchesList.AddRange(items);
        }

        public void HideOrShowContact(BranchContact contact)
        {
            if (_previousContact == contact)
            {
                //clicking twice on same item will hide it
                contact.IsSelected = !contact.IsSelected;
                UpdateContacts(contact);

            }
            else
            {
                if (_previousContact != null)
                {
                    //hide previous selected item
                    _previousContact.IsSelected = false;
                    UpdateContacts(_previousContact);
                }
                //show selected item
                contact.IsSelected = true;
                UpdateContacts(contact);
            }

            _previousContact = contact;
        }

        private void UpdateContacts(BranchContact contact)
        {
            var index = BranchesList.IndexOf(contact);
            BranchesList.Remove(contact);
            BranchesList.Insert(index, contact);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
