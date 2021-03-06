﻿using System;
using System.Collections.Generic;

namespace PokerPlayer.Generated
{
    // Generated from https://github.com/lean-poker/poker-croupier/wiki/Player-API 
    public class Rootobject
    {
        public Player[] players { get; set; }
        public int small_blind { get; set; }
        public int orbits { get; set; }
        public int dealer { get; set; }
        public Card[] community_cards { get; set; }

        public int current_buy_in { get; set; }

        public int pot { get; set; }
    }

    public class Player
    {
        public string name { get; set; }
        public int stack { get; set; }
        public string status { get; set; }
        public int bet { get; set; }
        public Card[] hole_cards { get; set; }
        public string version { get; set; }
        public int id { get; set; }
    }

    public class Card : IComparable<Card>
    {
        public string rank { get; set; }
        public string suit { get; set; }

        public Card()
        {
            
        }

        public Card(string suit, string rank)
        {
            if (suit.ToLower() == "s")
            {
                this.suit = "spades";
            }

            if (suit.ToLower() == "h")
            {
                this.suit = "hearts";
            }

            if (suit.ToLower() == "d")
            {
                this.suit = "diamonds";
            }

            if (suit.ToLower() == "c")
            {
                this.suit = "clubs";
            }

            this.rank = rank.ToUpperInvariant();
        }

        private Dictionary<string, int> ranks = new Dictionary<string, int>()
                                                    {
                                                        { "2", 2},
                                                        { "3", 3},
                                                        { "4", 4},
                                                        { "5", 5},
                                                        { "6", 6},
                                                        { "7", 7},
                                                        { "8", 8},
                                                        { "9", 9},
                                                        { "10", 10},
                                                        { "J", 11},
                                                        { "Q", 12},
                                                        { "K", 13},
                                                        { "A", 14},
                                                    };

        public int CompareTo(Card other)
        {
            return ranks[this.rank].CompareTo(ranks[other.rank]);
        }
    }

    //public class GameStateOLD
    //{
    //    public int small_blind { get; set; }
    //    public int current_buy_in { get; set; }
    //    public int pot { get; set; }
    //    public int minimum_raise { get; set; }
    //    public int dealer { get; set; }
    //    public int orbits { get; set; }
    //    public int in_action { get; set; }
    //    public Player[] players { get; set; }
    //    public Community_Cards[] community_cards { get; set; }
    //}

    //public class Player
    //{
    //    public int id { get; set; }
    //    public string name { get; set; }
    //    public string status { get; set; }
    //    public string version { get; set; }
    //    public int stack { get; set; }
    //    public int bet { get; set; }
    //    public Hole_Cards[] hole_cards { get; set; }
    //}

    //public class Hole_Cards
    //{
    //    public string rank { get; set; }
    //    public string suit { get; set; }
    //}

    //public class Community_Cards
    //{
    //    public string rank { get; set; }
    //    public string suit { get; set; }
    //}
}
