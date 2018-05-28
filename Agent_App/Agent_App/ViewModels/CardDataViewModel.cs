using System;
using System.Collections.Generic;
using System.Text;
using Agent_App.Models;
using System.Collections.ObjectModel;


namespace Agent_App.ViewModels
{
    public class CardDataViewModel
    {
        private CardDataModel _previousCard;
        public ObservableCollection<CardDataModel> CardDataCollection { get; set; }

        public object SelectedItem { get; set; }

        public CardDataViewModel()
        {
            //CardDataCollection = new List<CardDataModel>();
            GenerateCardModel();
        }

        private void GenerateCardModel()
        {
            // for (var i = 0; i < 10; i++)
            {
                CardDataCollection = new ObservableCollection<CardDataModel>
                {
                    new CardDataModel
                    {
                     HeadTitle = " Start Date - 30th Sep 2011 ",

                     HeadLines="Insured Name - Mr. S. T. Perera" ,
                     ProfileImage = "car.png",
                     HeadLinesDesc = "This is a sample Description. Include anything that describes the policy",

                    },



                     new CardDataModel
                    {
                     HeadTitle = " Start Date - 15th Jan 1998 ",

                     HeadLines="Insured Name - Mrs. T. Karunasena" ,
                     ProfileImage = "health.png",
                     HeadLinesDesc = "This is a sample Description. Include anything that describes the policy",
                     },



                      new CardDataModel
                    {
                    HeadTitle = " Start Date - 01st July 2010 ",

                     HeadLines="Insured Name - Mr. Ruwan Kumara" ,
                     ProfileImage = "home.png",
                     HeadLinesDesc = "This is a sample Description. Include anything that describes the policy",
                      },


                       new CardDataModel
                    {
                    HeadTitle = " Start Date - 19th May 2011 ",

                     HeadLines="Insured Name - Mr. N. Wimalarathne" ,
                     ProfileImage = "life.png",
                     HeadLinesDesc = "This is a sample Description. Include anything that describes the policy",

                       },


                        new CardDataModel
                    {
                    HeadTitle = " Start Date - 12th Jan 2008 ",

                     HeadLines="Insured Name - Mr. K. P. Nawarathna" ,
                     ProfileImage = "car.png",
                     HeadLinesDesc = "This is a sample Description. Include anything that describes the policy",
                        },


                         new CardDataModel
                    {
                      HeadTitle = " Start Date - 10th Sep 2003 ",

                     HeadLines="Insured Name - Mrs. Malini Fernando" ,
                     ProfileImage = "health.png",
                     HeadLinesDesc = "This is a sample Description. Include anything that describes the policy",
                         },


                          new CardDataModel
                    {
                     HeadTitle = " Start Date - 08th Mar 2007 ",

                     HeadLines="Insured Name - Mr. D. L. Dewaraja" ,
                     ProfileImage = "home.png",
                     HeadLinesDesc = "This is a sample Description. Include anything that describes the policy",
                          },
                   
                 //   AlertColor =  Color.Green : Color.Blue,    This can be added to set alert dialog inside card data model
                };

            }
        }

        public void HideOrShowCard(CardDataModel card)
        {

            if (_previousCard == card)
            {
                //clicking twice on same item will hide it
                card.IsVisible = !card.IsVisible;
                UpdateCards(card);
            }
            else
            {
                if (_previousCard != null)
                {
                    //hide previous selected item
                    _previousCard.IsVisible = false;
                    UpdateCards(_previousCard);
                }
                //show selected item
                card.IsVisible = true;
                UpdateCards(card);
            }

            _previousCard = card;
        }

        private void UpdateCards(CardDataModel card)
        {
            var index = CardDataCollection.IndexOf(card);
            CardDataCollection.Remove(card);
            CardDataCollection.Insert(index, card);
        }
    }
}
