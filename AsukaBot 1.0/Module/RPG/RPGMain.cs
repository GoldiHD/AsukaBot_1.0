﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using AsukaBot_1._0.Module.RPG.Logic;
using AsukaBot_1._0.Module.RPG.Logic.Items;
using AsukaBot_1._0.Module.RPG.Logic.Questing;
using AsukaBot_1._0.Classes;
using AsukaBot_1._0.Module.RPG.Logic.Classes;
using AsukaBot_1._0.Module.RPG.Logic.Guilds;

namespace AsukaBot_1._0.Module.Music.Logic
{
    public class RPGMain : ModuleBase<SocketCommandContext>
    {

        public Inventory CheckUpList = new Inventory();
        private static List<Player> AllPlayers;
        private bool Loaded = false;
        private static GuildManager guildmanager;

        public RPGMain()
        {
            SingleTon.SetRPG(this);
            if (AllPlayers == null)
            {
                AllPlayers = new List<Player>();
                AllPlayers.Add(new Player());
                AllPlayers[0].GiveNewQuestManager();
            }
            if (guildmanager == null)
            {
                guildmanager = new GuildManager();
            }
        }

        public void AddPlayer(Player LoadedUser)
        {
            if (AllPlayers == null)
            {
                AllPlayers = new List<Player>();
            }
            AllPlayers.Add(LoadedUser);
        }

        public List<Player> GetPlayers()
        {
            if (AllPlayers != null)
            {
                return AllPlayers;
            }
            else
            {
                return new List<Player>();
            }
        }

        [Command("RPGStartup")]
        public async Task Rpgstartup()
        {
            if (Loaded == false)
            {
                SingleTon.SetRPG(this);
                SaveLoadRPGData Controler = new SaveLoadRPGData();
                Controler.LoadData();
                Loaded = true;
                await ReplyAsync("Dataloaded");
            }
            else
            {
                await ReplyAsync("Data already loaded");
            }
        }

        [Command("Save")]
        public async Task SaveTemp()
        {
            if (Context.User.Username == "Goldi")
            {
                SingleTon.SetRPG(this);
                SaveLoadRPGData Controler = new SaveLoadRPGData();
                Controler.SaveData();
                await ReplyAsync("DataSave");
            }
        }

        #region Items

        [Command("_inv")]
        public async Task InventoryCheck()
        {
            EmbedBuilder builder = new EmbedBuilder();
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await ReplyAsync("", false, builder.WithTitle("[ERROR] You're account wasn't instanceiated yet").Build());
            }
            else
            {
                builder.AddField("Gold", AllPlayers[temp].GetInventory().GetGold());
                if (AllPlayers[temp].GetInventory().GetTheInventory().Count > 0)
                {

                    builder.AddField("Inventory", StackItem(AllPlayers[temp].GetInventory().GetTheInventory()));
                }
                else
                {
                    builder.AddField("Inventory", "Empty");
                }
                builder.WithFooter(new EmbedFooterBuilder().WithText(Context.User.Username));
                await ReplyAsync("", false, builder.Build());

            }
        }

        [Command("_itemlist")]
        public async Task Itemlist()
        {
            string data = GetAllItems();
            await Context.Channel.SendMessageAsync(data);
        }

        [Command("_itemwiki")]
        public async Task ItemWiki()
        {
            EmbedBuilder builder = new EmbedBuilder();
            await ReplyAsync("", false, builder.AddField("Item wiki", @"https://docs.google.com/spreadsheets/d/1XEya_u-TRM1W5NMBb9N0wAg9mCRh51BxGFGLxSznJG8/edit?usp=sharing").Build());
        }

        [Command("_iteminfo")]
        public async Task ItemInfo(string para1, string para2 = null, string para3 = null, string para4 = null)
        {
            string temp = ConnectWords(new List<string> { para1, para2, para3, para4 });
            bool ItemExsists = false;
            string data = "";
            EmbedBuilder builder = new EmbedBuilder();
            for (int i = 0; i < CheckUpList.GetAllItemsList().Count; i++)
            {
                if (temp.ToLower() == CheckUpList.GetAllItemsList()[i].Getname().ToLower())
                {
                    WeaponsItem Weapon;
                    ArmorItem Armor;
                    CraftingItem Craft;
                    Consumable Consume;

                    ItemExsists = true;
                    try
                    {
                        if (CheckUpList.GetAllItemsList()[i] is WeaponsItem)
                        {
                            Weapon = (WeaponsItem)CheckUpList.GetAllItemsList()[i];
                            builder.AddField("Item", Weapon.Getname());
                            builder.AddField("Flavor text", Weapon.GetFlavorText());
                            builder.AddField("Price", Weapon.GetPrice());
                            builder.AddField("Damage", Weapon.GetDamge());
                            data = "Item: " + Weapon.Getname() + "\n" + "Flavor text: " + Weapon.GetFlavorText() + "\n" + "Damage: " + Weapon.GetDamge() + "\n" + "Physical damge type: " + Weapon.GetPhyDamgeType() + "\n" + "Value: " + Weapon.GetPrice() + "\n" + "Buyable: " + Weapon.GetBuyableState() + "\n" + "Quest item : " + Weapon.GetQuestState() + "\n" + "Crafting material(s): " + Weapon.GetCraftingMaterial() + "\n" + "Rarity: " + Weapon.GetRarity();
                        }

                        else if (CheckUpList.GetAllItemsList()[i] is ArmorItem)
                        {
                            Armor = (ArmorItem)CheckUpList.GetAllItemsList()[i];
                            data = "Item: " + Armor.Getname() + "\n" + "Flavor text: " + Armor.GetFlavorText() + "\n" + "AC: " + Armor.GetAC() + "\n" + "Armor type: " + Armor.GetArmorType() + "\n" + "Price: " + Armor.GetPrice() + "\n" + "Buyable: " + Armor.GetBuyableState() + "\n" + "Quest item: " + Armor.GetQuestState() + "\n" + "Crafting material(s): " + Armor.GetCraftingMaterial() + "\n" + "Rarity" + Armor.GetRarity();
                        }
                        else if (CheckUpList.GetAllItemsList()[i] is CraftingItem)
                        {
                            Craft = (CraftingItem)CheckUpList.GetAllItemsList()[i];
                            data = "Item: " + Craft.Getname() + "\n" + "Flavor text: " + Craft.GetFlavorText() + "\n " + "Rarity: " + Craft.GetRarity();
                        }
                        else if (CheckUpList.GetAllItemsList()[i] is Consumable)
                        {
                            Consume = (Consumable)CheckUpList.GetAllItemsList()[i];
                            data = "Item: " + Consume.Getname() + "\n" + "Flavor Text: " + Consume.GetFlavorText() + "\n" + "Strenght: " + Consume.GetEffect().GetStrenght() + "\n" + "Rarity: " + Consume.GetRarity();
                        }
                        else
                        {

                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    await ReplyAsync("", false, builder.Build());
                }
            }

            if (ItemExsists == false)
            {
                await ReplyAsync("Item [" + temp + "] does not exists");
            }
        }

        [Command("_equip")]
        public async Task EquipGear(string para1, string para2 = null, string para3 = null, string para4 = null)
        {
            string temp = ConnectWords(new List<string> { para1, para2, para3, para4 });
            bool doesitemExsits = false;

            for (int i = 0; i < CheckUpList.GetAllItemsList().Count; i++)
            {
                if (temp.ToLower() == CheckUpList.GetAllItemsList()[i].Getname().ToLower())
                {
                    doesitemExsits = true;
                }
            }

            if (doesitemExsits)
            {
                int user;

                user = DoIExist(Context.User.Username);
                if (user == -1)
                {
                    await Context.Channel.SendMessageAsync("error please try the command again");
                }
                else
                {
                    if (AllPlayers[user].GetInventory().DoesTimeExistsInInventory(temp))
                    {
                        if (AllPlayers[user].GetInventory().DoesTimeExistsInInventory(temp))
                        {
                            if (AllPlayers[user].GetInventory().GetinventoryItemByName(temp) is WeaponsItem || AllPlayers[user].GetInventory().GetinventoryItemByName(temp) is MagicAttacks)
                            {
                                AllPlayers[user].AssignWeapon(AllPlayers[user].GetInventory().GetinventoryItemByName(temp));
                                await Context.Channel.SendMessageAsync(temp + " have been equipped");
                            }
                            else if (AllPlayers[user].GetInventory().GetinventoryItemByName(temp) is ArmorItem)
                            {
                                AllPlayers[user].AssignArmor(AllPlayers[user].GetInventory().GetinventoryItemByName(temp));
                                await Context.Channel.SendMessageAsync(temp + " have been equipped");
                            }
                            else
                            {
                                await Context.Channel.SendMessageAsync("You can't equip " + AllPlayers[user].GetInventory().GetinventoryItemByName(temp));
                            }
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("You don't currently have that item");
                        }
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("You don't have that item");
                    }
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("error the given item doesn't exists");
            }
        }

        [Command("_use")]
        public async Task UseItem(string para1, string para2 = null, string para3 = null, string para4 = null)
        {
            bool doesitemExsits = false;
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("Your player wasn't ready");
            }
            else
            {

                string temp2 = ConnectWords(new List<string> { para1, para2, para3, para4 });

                for (int i = 0; i < CheckUpList.GetAllItemsList().Count; i++)
                {
                    if (temp2.ToLower() == CheckUpList.GetAllItemsList()[i].Getname().ToLower())
                    {
                        doesitemExsits = true;
                    }
                }
                if (doesitemExsits)
                {
                    if (AllPlayers[temp].GetInventory().DoesTimeExistsInInventory(temp2))
                    {
                        if (AllPlayers[temp].GetInventory().GetAllItemsList()[AllPlayers[temp].GetInventory().GetItemByName(temp2)] is Consumable)
                        {
                            await Context.Channel.SendMessageAsync(AllPlayers[temp].UseConsumable(temp2));
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("You can only use consumables");
                        }
                    }
                }

            }
        }

        [Command("_give")] //dev tool
        public async Task Give(string para1, string para2 = null, string para3 = null, string para4 = null)
        {
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("Your player wasn't instancitated");
            }
            else
            {
                if (Context.User.Username == "Goldi")
                {
                    string temp2 = ConnectWords(new List<string> { para1, para2, para3, para4 });

                    AllPlayers[temp].GetInventory().GivePlayerItem(CheckUpList.GetAllItemsList()[AllPlayers[temp].GetInventory().GetItemByName(temp2)]);
                    await Context.Channel.SendMessageAsync("Goldi spawned: " + temp2);
                }
                else
                {
                    await Context.Channel.SendMessageAsync("You don't have the correct access level to use this command");
                }
            }
        }

        #endregion

        #region Combat

        [Command("_encounter")]
        public async Task EnterEncounter()
        {
            EmbedBuilder builder = new EmbedBuilder();
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("Error, please try again");
            }
            else
            {
                if (AllPlayers[temp].GetPlayerState() == PlayerStates.Rest)
                {
                    builder.WithTitle("Encounter").WithDescription(Context.User.Username).WithColor(Color.Blue);
                    AllPlayers[temp].SetPlayerStates(PlayerStates.Encounter);
                    AllPlayers[temp].SetQuestManager(new QuestManager());
                    AllPlayers[temp].GetQuestManager().StartAdventure(AllPlayers[temp], AllPlayers[temp].GetPlayerState());
                    builder.AddField("Enemy", AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetName());
                    await ReplyAsync("", false, builder.Build());
                }
                else
                {
                    await Context.Channel.SendMessageAsync("you need to be out of an adventure to enter another");
                }
            }
        }

        [Command("_dungeon")]
        public async Task EnterDungeon()
        {

            EmbedBuilder builder = new EmbedBuilder();
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("Error, please try again");
            }
            else
            {
                if (AllPlayers[temp].GetPlayerState() == PlayerStates.Rest)
                {
                    builder.WithTitle("Dungeon").WithDescription(Context.User.Username).WithColor(Color.Blue);
                    AllPlayers[temp].SetPlayerStates(PlayerStates.Dungeon);
                    AllPlayers[temp].SetQuestManager(new QuestManager());
                    AllPlayers[temp].GetQuestManager().StartAdventure(AllPlayers[temp], AllPlayers[temp].GetPlayerState());
                    builder.AddField("Enemy", AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetName());
                    await ReplyAsync("", false, builder.Build());
                }
                else
                {
                    await ReplyAsync("you need to be out of an adventure to enter another");
                }
            }
        }

        [Command("_pvp")]
        public async Task EnterPvp(string username)
        {
            EmbedBuilder builder = new EmbedBuilder();
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("Error, please try again");
            }
            else
            {
                if (AllPlayers[temp].GetPlayerState() == PlayerStates.Rest)
                {
                    builder.WithTitle("Pvp").WithDescription(Context.User.Username).WithColor(Color.Blue);
                    bool UserExists = false;
                    Player defender = null;
                    for (int i = 1; i < AllPlayers.Count; i++)
                    {
                        if (AllPlayers[i].GetUsername().ToLower() == username.ToLower())
                        {
                            defender = AllPlayers[i];
                            UserExists = true;
                        }
                    }

                    if (UserExists)
                    {
                        if (defender.GetPlayerState() == PlayerStates.Rest)
                        {
                            AllPlayers[temp].SetPlayerStates(PlayerStates.Pvp);
                            AllPlayers[temp].SetQuestManager(new QuestManager());
                            AllPlayers[temp].GetQuestManager().StartAdventure(AllPlayers[temp], PlayerStates.Pvp, defender);
                            Console.WriteLine(AllPlayers[temp].GetUsername() + " is waiting on " + defender.GetUsername() + " to accept his fighting request(!_accept " + AllPlayers[temp].GetUsername() + ")", " ");
                            builder.AddField("PVP", AllPlayers[temp].GetUsername() + " is waiting on " + defender.GetUsername() + " to accept his fighting request(!_accept " + AllPlayers[temp].GetUsername() + ")");
                        }
                        else
                        {
                            builder.AddField("[Error]", "Player is busy and can't fight with you");
                        }
                    }
                    else
                    {
                        builder.AddField("[Error]", "Player: " + username + " doesn't exist");
                    }
                }
                else
                {
                    builder.AddField("[Error]", "you need to be out of an adventure to enter another");
                }
                await ReplyAsync("hel", false, builder.Build());
            }
        }

        [Command("_accept")]
        public async Task AcceptChallenege(string username = null)
        {
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("Error, please try again");
            }
            else
            {
                EmbedBuilder builder = new EmbedBuilder();
                if (AllPlayers[temp].GetPlayerState() == PlayerStates.Rest)
                {
                    if (username == null)
                    {
                        builder.AddField("[Error]", "Player doesn't exist");
                    }
                    else
                    {
                        for (int x = 0; x < AllPlayers[0].GetQuestManager().GetStoryMaker().GetPlayerListRequest().Count; x++)
                        {
                            if (AllPlayers[0].GetQuestManager().GetStoryMaker().GetPlayerListRequest()[x].GetAttacker().GetUsername() == username)
                            {
                                AllPlayers[0].GetQuestManager().GetStoryMaker().GetPlayerListRequest()[x].acceptRequest();
                                AllPlayers[0].GetQuestManager().GetStoryMaker().ChangeRequestToCombat(x);
                                builder.AddField("PVP", "Match have been started");
                            }
                            else
                            {
                                builder.AddField("PVP", "You have no challenge from him");
                            }
                        }
                    }
                }
                else
                {
                    builder.AddField("[ERROR]", "You're too busy to accept the request right now");
                }
                await ReplyAsync("test", false, builder.Build());
            }
        }

        [Command("_escape")]
        public async Task Escape()
        {
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("Error, please try again");
            }
            else
            {
                if (AllPlayers[temp].GetPlayerState() != PlayerStates.Rest)
                {
                    AllPlayers[temp].SetPlayerStates(PlayerStates.Rest);
                    await Context.Channel.SendMessageAsync("you have successfully escaped");
                    AllPlayers[temp].HealToMax();
                    if (AllPlayers[temp].GetPVPCombatControler() != null)
                    {
                        AllPlayers[temp].GetPVPCombatControler().EndBattle();
                    }
                    AllPlayers[temp].SetQuestManager(null);
                }
                else
                {
                    await Context.Channel.SendMessageAsync("you're currently not in any kind of combat");
                }

            }
        }

        [Command("_attack")]
        public async Task Attack()
        {
            EmbedBuilder builder = new EmbedBuilder();
            Stats Statsholder;
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await ReplyAsync("", false, builder.WithTitle("[ERROR] Acount haven't been created yet, try again").WithFooter(new EmbedFooterBuilder().WithText(Context.User.Username)).Build());
            }
            else
            {
                Statsholder = AllPlayers[temp].GetStats();
                switch (AllPlayers[temp].GetPlayerState())
                {
                    case PlayerStates.Rest:
                        await Context.Channel.SendMessageAsync("You're not in combat and so can't attack anything");
                        break;

                    case PlayerStates.Encounter:
                        if (AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy() == null)
                        {
                            await Context.Channel.SendMessageAsync("You're not in combat and so can't attack anything");
                            AllPlayers[temp].SetPlayerStates(PlayerStates.Rest);
                        }
                        else
                        {
                            builder.AddField("Your Attack", AllPlayers[temp].Attack()).WithColor(Color.Red);
                            builder.AddField("Counter attack", AllPlayers[temp].CounterAttack());
                            builder.AddField("Health", Statsholder.GetVitallity().GetMyHealth() + "/" + Statsholder.GetVitallity().GetMyMaxHealth());
                            if (AllPlayers[temp].GetQuestManager().GetStoryMaker().GetBattles() > 0)
                            {
                                builder.AddField("Battles left", AllPlayers[temp].GetQuestManager().GetStoryMaker().GetBattles());
                            }

                            if (AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetHP() > 0)
                            {
                                builder.AddField(AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetName() + "'s health", AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetHP() + "/" + AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetMaxHP());
                            }
                            else
                            {
                                builder.AddField(AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetName() + "'s health", "0" + "/" + AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetMaxHP());

                            }

                            if (AllPlayers[temp].GetStats().GetVitallity().GetMyHealth() >= 1)
                            {
                                if (AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetHP() <= 0)
                                {
                                    if (AllPlayers[temp].AddXP(AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetXP() * 2))
                                    {
                                        builder.AddField("Xp gain", AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetXP() * 2 + " level up");
                                    }
                                    else
                                    {
                                        builder.AddField("Xp gain", AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetXP() * 2);
                                    }
                                    builder.AddField("Loot", StackItem(AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetLoot(AllPlayers[temp])));
                                    try
                                    {
                                        builder.AddField("Gold", AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetGold(AllPlayers[temp], 1)).ToString();
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
                                    AllPlayers[temp].SetPlayerStates(PlayerStates.Rest);
                                    AllPlayers[temp].GetStats().GetVitallity().GainFullHealth();
                                    AllPlayers[temp].GetQuestManager().GetStoryMaker().RemoveAndUpdateList();
                                    AllPlayers[temp].SetQuestManager(null);
                                }

                            }
                            else
                            {
                                if (AllPlayers[temp].GetHardcoreState())
                                {
                                    builder.AddField("Hardcore death", "you have died on Hardcore, so your character i now no more");
                                    AllPlayers.RemoveAt(temp);
                                }
                                else
                                {
                                    builder.AddField("Dead", "you have died and will escape the mission with no loot and lost a bit of the gold you had on you");
                                    AllPlayers[temp].GetInventory().SetGold((int)(AllPlayers[temp].GetInventory().GetGold() - (AllPlayers[temp].GetInventory().GetGold() * 0.2f)));
                                    AllPlayers[temp].SetPlayerStates(PlayerStates.Rest);
                                    AllPlayers[temp].SetQuestManager(null);
                                    AllPlayers[temp].GetStats().GetVitallity().GainFullHealth();
                                }
                            }
                        }
                        break;


                    case PlayerStates.Dungeon:
                        if (AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy() == null)
                        {
                            builder.AddField("You're not in combat and so can't attack anything", "");
                            AllPlayers[temp].SetPlayerStates(PlayerStates.Rest);
                        }
                        else
                        {
                            builder.AddField("Your Attack", AllPlayers[temp].Attack()).WithColor(Color.Red);
                            builder.AddField("Counter attack", AllPlayers[temp].CounterAttack());
                            if (Statsholder.GetVitallity().GetMyHealth() < 0)
                            {
                                builder.AddField("Health", "0/" + Statsholder.GetVitallity().GetMyMaxHealth());
                            }
                            else
                            {
                                builder.AddField("Health", Statsholder.GetVitallity().GetMyHealth() + "/" + Statsholder.GetVitallity().GetMyMaxHealth());
                            }
                            if (AllPlayers[temp].GetQuestManager().GetStoryMaker().GetBattles() > 0)
                            {
                                builder.AddField("Battles left", AllPlayers[temp].GetQuestManager().GetStoryMaker().GetBattles());
                            }
                            if (AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetHP() > 0)
                            {
                                builder.AddField(AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetName() + "'s health", AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetHP() + "/" + AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetMaxHP());
                            }
                            else
                            {
                                builder.AddField(AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetName() + "'s health", "0/" + AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetMaxHP());
                            }

                            if (AllPlayers[temp].GetStats().GetVitallity().GetMyHealth() >= 1)
                            {
                                if (AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetHP() <= 0)
                                {
                                    if (AllPlayers[temp].AddXP(AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetXP() * 2))
                                    {
                                        builder.AddField("Xp gain", AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetXP() * 2 + " level up");
                                    }
                                    else
                                    {
                                        builder.AddField("Xp gain", AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetXP() * 2);
                                    }
                                    builder.AddField("Loot", StackItem(AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetLoot(AllPlayers[temp])));
                                    try
                                    {
                                        builder.AddField("Gold", AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetGold(AllPlayers[temp], 2)).ToString();
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }

                                    AllPlayers[temp].GetQuestManager().GetStoryMaker().RemoveAndUpdateList();
                                    if (AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy() == null)
                                    {
                                        builder.WithTitle("Dungeon complete");
                                        AllPlayers[temp].SetPlayerStates(PlayerStates.Rest);
                                        AllPlayers[temp].SetQuestManager(null);
                                        AllPlayers[temp].GetStats().GetVitallity().GainFullHealth();
                                    }
                                }
                            }
                            else
                            {
                                if (AllPlayers[temp].GetHardcoreState())
                                {
                                    builder.AddField("Hardcore death", "you have died on Hardcore, so your character i now no more");
                                    AllPlayers.RemoveAt(temp);
                                }
                                else
                                {
                                    builder.AddField("Dead", "you have died and will escape the mission with no loot");
                                    AllPlayers[temp].SetPlayerStates(PlayerStates.Rest);
                                    AllPlayers[temp].SetQuestManager(null);
                                    AllPlayers[temp].GetStats().GetVitallity().GainFullHealth();
                                }
                            }
                        }

                        break;

                    case PlayerStates.CollabBoss:

                        break;

                    case PlayerStates.Campaign:

                        break;


                    case PlayerStates.Pvp:
                        if (AllPlayers[temp].GetPVPCombatControler() != null)
                        {

                            builder.AddField("Attack", (AllPlayers[temp].GetPVPCombatControler().AttackOtherPlayer(AllPlayers[temp])));
                            if (AllPlayers[temp].GetPVPCombatControler().GetTheOtherPlayer(AllPlayers[temp]).GetStats().GetVitallity().GetMyHealth() < 1)
                            {
                                builder.AddField("Battle Resualt", AllPlayers[temp].GetUsername() + " won over " + AllPlayers[temp].GetPVPCombatControler().GetTheOtherPlayer(AllPlayers[temp]).GetUsername() + ", and won 10 arena points");
                                AllPlayers[temp].GetPVPCombatControler().EndBattle();
                                StoryMaker.PlayerBattleControler.Remove(AllPlayers[temp].GetPVPCombatControler());
                                AllPlayers[temp].AreanaPoints += 10;
                            }

                        }
                        else
                        {
                            builder.AddField("[Error]", "Other user have not accepted the request");
                        }
                        break;
                }
                builder.WithFooter(new EmbedFooterBuilder().WithText(Context.User.Username));
                if (builder.Fields.Count > 7)
                {
                    Console.WriteLine("Error inbound");
                }
                await ReplyAsync("", false, builder.Build());
            }
        }

        [Command("_combat")]
        public async Task Combat()
        {
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("Error, please try again");
            }
            else
            {
                if (AllPlayers[temp].GetPlayerState() == PlayerStates.Rest)
                {
                    await Context.Channel.SendMessageAsync("You're currently not in combat");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("You're currently in combat \n You're in : " + AllPlayers[temp].GetPlayerState() + "\n-------------- \n    " + Context.User.Username + "\n    Health: " + AllPlayers[temp].GetStats().GetVitallity().GetMyHealth() + "/" + AllPlayers[temp].GetStats().GetVitallity().GetMyMaxHealth() + "\n    MP: " + AllPlayers[temp].GetStats().GetMagic().GetMana() + "/" + AllPlayers[temp].GetStats().GetMagic().GetMaxMana() + "\n-------------- \n    " + AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetName() + "\n    Health: " + AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetHP() + "/" + AllPlayers[temp].GetQuestManager().GetStoryMaker().GetEnemy().GetMaxHP());
                }
            }
        }



        #endregion

        #region Stats Commands

        [Command("_hardcore")]
        public async Task toggleHardCore()
        {
            EmbedBuilder builder = new EmbedBuilder();
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                builder.AddField("Hardcore", "[Error]");
            }
            else
            {
                if (AllPlayers[temp].GetHardcoreState())
                {
                    AllPlayers[temp].SetHardcoreState(true);
                    builder.AddField("Hardcore", "You have set to, preper to die");
                }
                else
                {
                    builder.AddField("Hardcore", "You're already in hardcore mode");
                }
            }
            await ReplyAsync("Harcore", false, builder.Build());

        }

        [Command("_stats")]
        public async Task StatsDisplay()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Stats").Color = Color.Orange;
            builder.WithDescription(Context.User.Username);
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("Error please try again");
            }
            else
            {
                Stats Statsholder = AllPlayers[temp].GetStats();
                builder.AddInlineField("Class", AllPlayers[temp].GetClass().GetClassName());
                builder.AddInlineField("Power", Statsholder.GetPower().GetPowerLvl());
                builder.AddInlineField("Magic", Statsholder.GetMagic().GetMagicLvl());
                builder.AddInlineField("Dexterity", Statsholder.GetDexterity().GetDexterityLvl());
                builder.AddInlineField("Intelligence", Statsholder.GetIntellegence().GetIntellegencLvl());
                builder.AddInlineField("Vitallity", Statsholder.GetVitallity().GetVitallityLvl());
                builder.AddInlineField("Luck", Statsholder.GetLuck().GetLuckLvl());

                await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
        }

        [Command("_status")]
        public async Task StatusDisplay()
        {
            Stats Statsholder;
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("Sorry but your character didn't exists so we have now created one, and you can try again to display your stats");
            }
            else
            {
                Statsholder = AllPlayers[temp].GetStats();
                EmbedBuilder builder = new EmbedBuilder();
                builder.WithTitle("Status").WithDescription(Context.User.Username).WithColor(Color.Blue);
                builder.AddField("Health", Statsholder.GetVitallity().GetMyHealth() + "/" + Statsholder.GetVitallity().GetMyMaxHealth());
                builder.AddField("Armor", AllPlayers[temp].GetAC());
                builder.AddField("Level", AllPlayers[temp].GetPlayerLvl());
                builder.AddField("Exp", AllPlayers[temp].GetExpCurrent() + "/" + AllPlayers[temp].GetExpForNextLvl());
                builder.AddField("Available stats points", Statsholder.GetStatPoints());
                builder.AddField("Hardcore mode", AllPlayers[temp].GetHardcoreState());
                await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
        }

        [Command("_power")]
        public async Task PowerIncrease(int amount)
        {
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("you error have seemed to occurred, please try redoing your command");
            }
            else
            {
                if (amount <= AllPlayers[temp].GetStats().GetStatPoints())
                {
                    if (AllPlayers[temp].GetStats().GetPower().IncreaseStatLvl(amount))
                    {
                        AllPlayers[temp].GetStats().SetStatsPoints(amount);
                        await Context.Channel.SendMessageAsync("Power: " + AllPlayers[temp].GetStats().GetPower().GetPowerLvl() + "\nYou have " + AllPlayers[temp].GetStats().GetStatPoints() + " left now");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("you can't increase your stats by 0");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("please use a valid number, compared to the number of stat points you have left,\nYou have: " + AllPlayers[temp].GetStats().GetStatPoints() + " points left");
                }
            }
        }

        [Command("_pow")]
        public async Task PowIncrease(int amount)
        {
            await PowerIncrease(amount);
        }

        [Command("_magic")]
        public async Task MagicIncrease(int amount)
        {
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("you error have seemed to occurred, please try redoing your command");
            }
            else
            {
                if (amount <= AllPlayers[temp].GetStats().GetStatPoints())
                {
                    if (AllPlayers[temp].GetStats().GetMagic().IncreaseStatLvl(amount))
                    {
                        AllPlayers[temp].GetStats().SetStatsPoints(amount);
                        await Context.Channel.SendMessageAsync("Magic: " + AllPlayers[temp].GetStats().GetMagic().GetMagicLvl() + "\nYou have " + AllPlayers[temp].GetStats().GetStatPoints() + " left now");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("you can't increase your stats by 0");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("please use a valid number, compared to the number of stat points you have left,\nYou have: " + AllPlayers[temp].GetStats().GetStatPoints() + " points left");
                }
            }
        }

        [Command("_mag")]
        public async Task MagIncrease(int amount)
        {
            await MagicIncrease(amount);
        }

        [Command("_dexterity")]
        public async Task DexterityIncrease(int amonunt)
        {
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("you error have seemed to occurred, please try redoing your command");
            }
            else
            {
                if (amonunt <= AllPlayers[temp].GetStats().GetStatPoints())
                {
                    if (AllPlayers[temp].GetStats().GetDexterity().IncreaseStatLvl(amonunt))
                    {
                        AllPlayers[temp].GetStats().SetStatsPoints(amonunt);
                        await Context.Channel.SendMessageAsync("Dexterity: " + AllPlayers[temp].GetStats().GetDexterity().GetDexterityLvl() + "\nYou have " + AllPlayers[temp].GetStats().GetStatPoints() + " left now");

                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("you can't increase your stats by 0");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("please use a valid number, compared to the number of stat points you have left,\nYou have: " + AllPlayers[temp].GetStats().GetStatPoints() + " points left");
                }
            }
        }

        [Command("_dex")]
        public async Task DexIncrease(int amount)
        {
            await DexterityIncrease(amount);
        }

        [Command("_intellegenc")]
        public async Task IntellegenceIncrease(int amount)
        {
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("you error have seemed to occurred, please try redoing your command");
            }
            else
            {
                if (amount <= AllPlayers[temp].GetStats().GetStatPoints())
                {
                    if (AllPlayers[temp].GetStats().GetIntellegence().IncreaseStatLvl(amount))
                    {
                        AllPlayers[temp].GetStats().SetStatsPoints(amount);
                        await Context.Channel.SendMessageAsync("Intellegenc: " + AllPlayers[temp].GetStats().GetIntellegence().GetIntellegencLvl() + "\nYou have " + AllPlayers[temp].GetStats().GetStatPoints() + " left now");

                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("you can't increase your stats by 0");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("please use a valid number, compared to the number of stat points you have left,\nYou have: " + AllPlayers[temp].GetStats().GetStatPoints() + " points left");
                }
            }
        }

        [Command("_int")]
        public async Task IntIncrease(int amount)
        {
            await IntellegenceIncrease(amount);
        }

        [Command("_vitallity")]
        public async Task VitallityIncrease(int amount)
        {
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("you error have seemed to occurred, please try redoing your command");
            }
            else
            {
                if (amount <= AllPlayers[temp].GetStats().GetStatPoints())
                {
                    if (AllPlayers[temp].GetStats().GetVitallity().IncreaseStatLvl(amount))
                    {
                        AllPlayers[temp].GetStats().SetStatsPoints(amount);
                        await Context.Channel.SendMessageAsync("Vitallity: " + AllPlayers[temp].GetStats().GetVitallity().GetVitallityLvl() + "\nYou have " + AllPlayers[temp].GetStats().GetStatPoints() + " left now");

                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("you can't increase your stats by 0");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("please use a valid number, compared to the number of stat points you have left,\nYou have: " + AllPlayers[temp].GetStats().GetStatPoints() + " points left");
                }
            }
        }

        [Command("_vit")]
        public async Task VitIncrease(int amount)
        {
            await VitallityIncrease(amount);
        }

        [Command("_luck")]
        public async Task LuckIncrease(int amount)
        {
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("you error have seemed to occurred, please try redoing your command");
            }
            else
            {
                if (amount <= AllPlayers[temp].GetStats().GetStatPoints())
                {
                    if (AllPlayers[temp].GetStats().GetLuck().IncreaseStatLvl(amount))
                    {
                        AllPlayers[temp].GetStats().SetStatsPoints(amount);
                        await Context.Channel.SendMessageAsync("Luck: " + AllPlayers[temp].GetStats().GetLuck().GetLuckLvl() + "\nYou have " + AllPlayers[temp].GetStats().GetStatPoints() + " left now");

                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("you can't increase your stats by 0");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("please use a valid number, compared to the number of stat points you have left,\nYou have: " + AllPlayers[temp].GetStats().GetStatPoints() + " points left");
                }
            }
        }

        [Command("_class")]
        public async Task PickClass(string pickedclass)
        {
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("you error have seemed to occurred, please try redoing your command");
            }
            else
            {
                if (AllPlayers[temp].GetClass() is ClassLess)
                {
                    switch (pickedclass.ToLower())
                    {
                        case "warrior":
                            AllPlayers[temp].SetClass(new ClassWarior(AllPlayers[temp]));
                            await ReplyAsync("", false, new EmbedBuilder().AddField("Class", "Warrior").WithFooter(new EmbedFooterBuilder().WithText(Context.User.Username)).Build());
                            break;

                        case "paladin":
                            AllPlayers[temp].SetClass(new ClassRanger(AllPlayers[temp]));
                            await ReplyAsync("", false, new EmbedBuilder().AddField("Class", "Paladin").WithFooter(new EmbedFooterBuilder().WithText(Context.User.Username)).Build());
                            break;

                        case "rouge":
                            AllPlayers[temp].SetClass(new ClassRouge(AllPlayers[temp]));
                            await ReplyAsync("", false, new EmbedBuilder().AddField("Class", "Rouge").WithFooter(new EmbedFooterBuilder().WithText(Context.User.Username)).Build());
                            break;

                        case "cleric":
                            AllPlayers[temp].SetClass(new ClassCleric(AllPlayers[temp]));
                            await ReplyAsync("", false, new EmbedBuilder().AddField("Class", "cleric").WithFooter(new EmbedFooterBuilder().WithText(Context.User.Username)).Build());
                            break;

                        case "ranger":
                            AllPlayers[temp].SetClass(new ClassRanger(AllPlayers[temp]));
                            await ReplyAsync("", false, new EmbedBuilder().AddField("Class", "Ranger").WithFooter(new EmbedFooterBuilder().WithText(Context.User.Username)).Build());
                            break;

                        case "mage":
                            AllPlayers[temp].SetClass(new ClassMage(AllPlayers[temp]));
                            await ReplyAsync("", false, new EmbedBuilder().AddField("Class", "Mage").WithFooter(new EmbedFooterBuilder().WithText(Context.User.Username)).Build());
                            break;

                        default:
                            await Context.Channel.SendMessageAsync("Invalid input");
                            break;
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("You already have a class");
                }
            }
        }


        #endregion

        #region Market
        [Command("_CheckMarket")]
        public async Task CheckMarketItems(int Page = 1, string itemtype = null)
        {
            int counter = 0;
            const int PageSize = 5; //change the amount of stuff displayed on pages 
            List<BaseItem[]> HolderFinal = new List<BaseItem[]>();
            EmbedBuilder builder = new EmbedBuilder();
            List<BaseItem> Holder = new List<BaseItem>();
            ItemType PickedType = new ItemType();
            builder.WithTitle("Market");

            if (itemtype == null)
            {

                for (int i = 0; i < CheckUpList.GetAllItemsList().Count; i++)
                {
                    if (CheckUpList.GetAllItemsList()[i].GetBuyableState())
                    {
                        Holder.Add(CheckUpList.GetAllItemsList()[i]);
                    }
                }
                builder.WithTitle("Market");
                builder.WithDescription("filter: None");
                for (int i = 0; i < (int)(Math.Ceiling((double)Holder.Count / PageSize)); i++)
                {
                    HolderFinal.Add(new BaseItem[PageSize]);
                    for (int x = 0; x < PageSize; x++)
                    {
                        if (!(counter >= Holder.Count))
                        {
                            HolderFinal[i][x] = Holder[counter];
                            counter++;
                        }
                    }
                }
                if (Page == 0 || Page > HolderFinal.Count)
                {
                    await ReplyAsync("The give page number was not a vailid choice");
                }
                else
                {
                    foreach (BaseItem item in HolderFinal[Page - 1])
                    {
                        if (item != null)
                        {
                            builder.AddField(item.Getname(), "Price: " + item.GetPrice().ToString());
                        }
                    }
                    builder.WithFooter(new EmbedFooterBuilder().WithText("Page " + Page + "/" + HolderFinal.Count));
                }
                await ReplyAsync("", false, builder.Build());
            }
            else
            {
                switch (itemtype.ToLower())
                {
                    case "weapon":
                        builder.WithDescription("filter: Weapons");
                        PickedType = ItemType.Weapon;
                        break;

                    case "armor":
                        builder.WithDescription("filter: Armor");
                        PickedType = ItemType.Armor;
                        break;

                    case "consumable":
                        builder.WithDescription("filter: Consumable");
                        PickedType = ItemType.Consumable;
                        break;

                    case "craftingitem":
                        builder.WithDescription("filter: Crafting item");
                        PickedType = ItemType.Craftingitem;
                        break;

                    case "iron":
                        builder.WithDescription("filter: Iron");
                        PickedType = ItemType.Iron;
                        break;

                    case "steel":
                        builder.WithDescription("filter: Steel");
                        PickedType = ItemType.Steel;
                        break;

                    case "dragon":
                        builder.WithDescription("filter: Dragon");
                        PickedType = ItemType.Dragon;
                        break;

                    case "leather":
                        builder.WithDescription("filter: Leather");
                        PickedType = ItemType.Leather;
                        break;

                    case "light":
                        builder.WithDescription("filter: Light");
                        PickedType = ItemType.Light;
                        break;

                    case "medium":
                        builder.WithDescription("filter: Medium");
                        PickedType = ItemType.Medium;
                        break;

                    case "heavy":
                        builder.WithDescription("filter: Heavy");
                        PickedType = ItemType.Heavy;
                        break;

                    case "head":
                        builder.WithDescription("filter: Head");
                        PickedType = ItemType.Head;
                        break;

                    case "legs":
                        builder.WithDescription("filter: Legs");
                        PickedType = ItemType.Legs;
                        break;

                    case "hands":
                        builder.WithDescription("filter: hands");
                        PickedType = ItemType.Hands;
                        break;

                    case "chest":
                        builder.WithDescription("filter: Chest");
                        PickedType = ItemType.Chest;
                        break;

                    case "wood":
                        builder.WithDescription("filter: Wood");
                        PickedType = ItemType.Wood;
                        break;

                    case "metal":
                        builder.WithDescription("filter: Metal");
                        PickedType = ItemType.Metal;
                        break;

                    case "demon":
                        builder.WithDescription("filter: Demon");
                        PickedType = ItemType.Demon;
                        break;

                    case "slash":
                        builder.WithDescription("filter: Slash");
                        PickedType = ItemType.Slash;
                        break;

                    case "blunt":
                        builder.WithDescription("filter: Blunt");
                        PickedType = ItemType.Blunt;
                        break;

                    case "punture":
                        builder.WithDescription("filter: Punture");
                        PickedType = ItemType.Punture;
                        break;

                    case "heal":
                        builder.WithDescription("filter: Heal");
                        PickedType = ItemType.Heal;
                        break;

                    case "buff":
                        builder.WithDescription("filter: Buff");
                        PickedType = ItemType.Buff;
                        break;

                    case "food":
                        builder.WithDescription("filter: Food");
                        PickedType = ItemType.Food;
                        break;

                    default:
                        builder.WithDescription("no filter like that exists");
                        PickedType = ItemType.Default;
                        break;
                }
                for (int i = 0; i < CheckUpList.GetAllItemsList().Count - 1; i++)
                {
                    if (CheckUpList.GetAllItemsList()[i].GetBuyableState() == true)
                    {
                        List<ItemType> Temp = CheckUpList.GetAllItemsList()[i].GetItemType();
                        for (int x = 0; x <= Temp.Count - 1; x++)
                        {
                            if (Temp[x] == PickedType)
                            {
                                Holder.Add(CheckUpList.GetAllItemsList()[i]);
                            }
                        }
                    }
                }
                if (Holder.Count > PageSize)
                {

                    for (int i = 0; i < (int)(Math.Ceiling((double)Holder.Count / PageSize)); i++)
                    {
                        HolderFinal.Add(new BaseItem[5]);
                        for (int x = 0; x < PageSize; x++)
                        {
                            if (!(counter >= Holder.Count))
                            {
                                HolderFinal[i][x] = Holder[counter];
                                counter++;
                            }
                        }
                    }
                    if (Page == 0 || Page > HolderFinal.Count)
                    {
                        await ReplyAsync("The give page number was not a vailid choice");
                    }
                    else
                    {
                        foreach (BaseItem item in HolderFinal[Page - 1])
                        {
                            if (item != null)
                            {
                                builder.AddField(item.Getname(), "Price: " + item.GetPrice().ToString());
                            }
                        }
                        builder.WithFooter(new EmbedFooterBuilder().WithText("Page " + Page + "/" + HolderFinal.Count));
                    }
                }
                else
                {
                    for (int i = 0; i < Holder.Count; i++)
                    {
                        builder.AddField(Holder[i].Getname(), "Price: " + Holder[i].GetPrice().ToString());
                    }
                    builder.WithFooter(new EmbedFooterBuilder().WithText("Page 1/1"));
                }
                await ReplyAsync("", false, builder.Build());
            }



        }
        [Command("_MarketBuy")]
        public async Task BuyFromMarket(string para1, string para2 = null, string para3 = null, string para4 = null)
        {
            bool itemFound = false;
            EmbedBuilder builder = new EmbedBuilder();
            int temp = DoIExist(Context.User.Username);
            string ItemInput = ConnectWords(new List<string> { para1, para2, para3, para4 }).ToLower();
            if (temp == -1)
            {
                await ReplyAsync("", false, builder.WithTitle("[ERROR] You're account wasn't instanceiated yet").Build());
            }
            else
            {
                for (int i = 0; i < CheckUpList.GetAllItemsList().Count; i++)
                {
                    if (ItemInput == CheckUpList.GetAllItemsList()[i].Getname().ToLower())
                    {
                        itemFound = true;
                        if (CheckUpList.GetAllItemsList()[i].GetBuyableState())
                        {
                            if (CheckUpList.GetAllItemsList()[i].GetPrice() <= AllPlayers[temp].GetInventory().GetGold())
                            {
                                AllPlayers[temp].GetInventory().GivePlayerItem(CheckUpList.GetAllItemsList()[i]);
                                AllPlayers[temp].GetInventory().BuyStuff(CheckUpList.GetAllItemsList()[i].GetPrice());
                                await ReplyAsync("", false, builder.WithTitle("You have bought").AddField(CheckUpList.GetAllItemsList()[i].Getname(), "Price: " + CheckUpList.GetAllItemsList()[i].GetPrice()).WithFooter(new EmbedFooterBuilder().WithText(Context.User.Username)));
                            }
                            else
                            {
                                await ReplyAsync("", false, builder.WithTitle("[ERORR] you don't have enought gold to buy this").WithFooter(new EmbedFooterBuilder().WithText(Context.User.Username)).Build());
                            }
                        }
                        else
                        {
                            await ReplyAsync("", false, builder.WithTitle("[ERORR] item is not for sale on the market").WithFooter(new EmbedFooterBuilder().WithText(Context.User.Username)).Build());
                        }
                    }
                }
            }
            if (itemFound == false)
            {
                await ReplyAsync("", false, builder.WithTitle("[ERROR] " + ItemInput + " does not exists").WithFooter(new EmbedFooterBuilder().WithText(Context.User.Username)).Build());
            }
        }

        [Command("_MarketSell")]
        public async Task SellOnMarket(int amount, [Remainder]string itemName)
        {
            bool itemInInventory = false;
            EmbedBuilder builder = new EmbedBuilder();
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await ReplyAsync("[ERROR]" + "You're account wasn't instanceiated yet");
            }
            else
            {
                IGuild hello;
                if (AllPlayers[temp].GetInventory().GetTheInventory().Count > 0)
                {
                    for (int i = 0; i < AllPlayers[temp].GetInventory().GetTheInventory().Count; i++)
                    {
                        if (itemName.ToLower() == AllPlayers[temp].GetInventory().GetTheInventory()[i].Getname().ToLower())
                        {
                            if (AllPlayers[temp].GetInventory().GetTheInventory()[i].GetBuyableState())
                            {
                                if (AllPlayers[temp].GetInventory().GetTheInventory().FindAll(x => x.Getname().ToLower() == itemName.ToLower()).Count >= amount)
                                {
                                    itemInInventory = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    builder.AddField("Nothing", "you don't even have anything to sell");
                }
            }

            if (itemInInventory)
            {
                for (int i = 0; i < amount; i++)
                {
                    AllPlayers[temp].GetInventory().GetTheInventory().Remove(AllPlayers[temp].GetInventory().GetTheInventory().Find(x => x.Getname() == itemName));

                }
                AllPlayers[temp].GetInventory().GiveGold(CheckUpList.GetAllItemsList().Find(x => x.Getname().ToLower() == itemName.ToLower()).GetPrice() * amount);
                builder.AddField("Item(s) sold", "Sold: " + itemName + " " + amount + " for " + (CheckUpList.GetAllItemsList().Find(x => x.Getname().ToLower() == itemName.ToLower()).GetPrice() * amount) + " Gold");
            }
            else
            {
                builder.AddField("[ERROR]", "the item can't be sold/do not exists, or you don' have enough of it");
            }
            builder.Footer = new EmbedFooterBuilder().WithText(Context.User.Username);
            await ReplyAsync("Market", false, builder.Build());
        }
        #endregion

        #region Farming
        [Command("_Mine")]
        public async Task Minning()
        {
            EmbedBuilder Builder = new EmbedBuilder();
            Builder.WithTitle("Mine");
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                Builder.AddField("Error please try again", "");
            }
            else
            {
                if (AllPlayers[temp].Mine() == 0)
                {
                    AllPlayers[temp].GetInventory().GetTheInventory().Add(AllPlayers[temp].GetInventory().GetAllItemsList()[AllPlayers[temp].GetInventory().GetItemByName("Iron")]);
                    Builder.AddField("You mined: ", "Iron");
                }
                else
                {
                    if ((300 - (AllPlayers[temp].Mine() / 1000)) > 60)
                    {
                        Builder.AddField("You need to wait", ((300 - (AllPlayers[temp].Mine() / 1000)) / 60).ToString("0") + " min");
                    }
                    else
                    {
                        Builder.AddField("You need to wait", (300 - (AllPlayers[temp].Mine() / 1000)).ToString("0") + " sec");
                    }
                }
            }
            Builder.WithFooter(new EmbedFooterBuilder().WithText(Context.User.Username));
            await ReplyAsync("", false, Builder.Build());
        }

        [Command("_Chop")]
        public async Task ChopWood()
        {
            EmbedBuilder Builder = new EmbedBuilder();
            Builder.WithTitle("Chop");
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                Builder.AddField("Error please try again", "");
            }
            else
            {
                if (AllPlayers[temp].Chop() == 0)
                {
                    AllPlayers[temp].GetInventory().GetTheInventory().Add(AllPlayers[temp].GetInventory().GetAllItemsList()[AllPlayers[temp].GetInventory().GetItemByName("Regular wood")]);
                    Builder.AddField("You chopped: ", " Regular wood");
                }
                else
                {
                    if ((300 - (AllPlayers[temp].Chop() / 1000)) > 60)
                    {
                        Builder.AddField("You need to wait", ((300 - (AllPlayers[temp].Chop() / 1000)) / 60).ToString("0") + " min");
                    }
                    else
                    {
                        Builder.AddField("You need to wait", (300 - (AllPlayers[temp].Chop() / 1000)).ToString("0") + " sec");
                    }
                }
            }
            Builder.WithFooter(new EmbedFooterBuilder().WithText(Context.User.Username));
            await ReplyAsync("", false, Builder.Build());
        }

        [Command("_Forage")]
        public async Task Forage()
        {
            EmbedBuilder Builder = new EmbedBuilder();
            Builder.WithTitle("Forage");
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                Builder.AddField("Error please try again", "");
            }
            else
            {
                if (AllPlayers[temp].Forage() == 0)
                {
                    AllPlayers[temp].GetInventory().GetTheInventory().Add(AllPlayers[temp].GetInventory().GetAllItemsList()[AllPlayers[temp].GetInventory().GetItemByName("Berries")]);
                    Builder.AddField("You chopped: ", "Berries");
                }
                else
                {
                    if ((300 - (AllPlayers[temp].Forage() / 1000)) > 60)
                    {
                        Builder.AddField("You need to wait", ((300 - (AllPlayers[temp].Forage() / 1000)) / 60).ToString("0") + " min");
                    }
                    else
                    {
                        Builder.AddField("You need to wait", (300 - (AllPlayers[temp].Forage() / 1000)).ToString("0") + " sec");
                    }
                }
            }
            Builder.WithFooter(new EmbedFooterBuilder().WithText(Context.User.Username));
            await ReplyAsync("", false, Builder.Build());
        }
        #endregion

        #region Guild
        [Command("TransferLeadership")]
        public async Task TransferGuildLeadership([Remainder]string guildmemeberName)
        {
            int temp = DoIExist(Context.Message.Author.Username);
            if (GuildManager.guilds.Count != 0)
            {
                if (AllPlayers[temp].MyGuild != null)
                {
                    if (AllPlayers[temp].MyGuild.Leader == AllPlayers[temp])
                    {
                        if(AllPlayers.Exists(x => x.GetUsername().ToLower().Equals(guildmemeberName.ToLower())))
                        {
                            AllPlayers[temp].MyGuild.SetLeader(AllPlayers[temp].MyGuild.guildMemebers.Find(x => x.User.GetUsername().ToLower() == guildmemeberName.ToLower()).User);
                            await ReplyAsync("Leadership has been transferd to " + AllPlayers[temp].MyGuild.guildMemebers.Find(x => x.User.GetUsername().ToLower() == guildmemeberName.ToLower()).User.GetUsername());
                        }
                    }
                }
            }
        }

        [Command("JoinGuild")]
        public async Task JoinGuild([Remainder] string guildname)
        {
            Guild EasyAccessGuildContainer;
            int temp = DoIExist(Context.Message.Author.Username);
            if (AllPlayers[temp].MyGuild == null)
            {
                if (GuildManager.guilds.Count > 0)
                {
                    if (GuildManager.guilds.Exists(x => x.guildName.ToLower().Equals(guildname.ToLower())))
                    {
                        EasyAccessGuildContainer = GuildManager.guilds.Find(x => x.guildName.ToLower().Equals(guildname.ToLower()));
                        if (EasyAccessGuildContainer.InviteOnly)
                        {
                            await ReplyAsync("This guild is invite only");
                        }
                        else
                        {
                            EasyAccessGuildContainer.guildMemebers.Add(new GuildMemebers(AllPlayers[temp], GuildRank.Recruit));
                            await ReplyAsync("You joined" + guildname);
                        }
                    }
                    else
                    {
                        await ReplyAsync("no guild with that name");
                    }
                }
                else
                {
                    await ReplyAsync("No guilds exists");
                }
            }
            else
            {
                await ReplyAsync("you're already in a guild");
            }
        }

        [Command("LeaveGuild")]
        public async Task LeaveGuild()
        {
            int temp = DoIExist(Context.Message.Author.Username);
            if (AllPlayers[temp].MyGuild == null)
            {
                await ReplyAsync("You're not currently in a guild");
            }
            else
            {
                AllPlayers[temp].MyGuild.guildMemebers.Remove(AllPlayers[temp].MyGuild.guildMemebers.Find(x => x.User == AllPlayers[temp]));
                AllPlayers[temp].MyGuild = null;
            }
        }

        [Command("CreateGuild")]
        public async Task CreateGuild([Remainder] string guildname)
        {
            int temp = DoIExist(Context.User.Username);
            if (AllPlayers[temp].GetPlayerLvl() >= 5)
            {
                if (AllPlayers[temp].GetInventory().GetGold() >= 500)
                {
                    guildmanager.CreateGuild(guildname, AllPlayers[temp]);
                }
                else
                {
                    await ReplyAsync("Sorry but you don't have enought for the 500 gold fee, there is for creating a guild");
                }
            }
            else
            {
                await ReplyAsync("Sorry but you're not high enought level to create a guild");
            }
        }

        #endregion

        #region Bank
        [Command("")]
        public async Task DepositToBank(int amount)
        {
            int temp = DoIExist(Context.Message.Author.Username);
        }
        #endregion

        private void CheckHealth(string username)
        {
            AllPlayers[DoIExist(username)].GetStats().GetVitallity().GetMyHealth();
        }

        private int DoIExist(string username)
        {
            if (AllPlayers == null)
            {
                AllPlayers = new List<Player>();
                AllPlayers.Add(new Player(username));
            }

            bool DoesPlayerExists = false;
            int Locations = 0;
            for (int i = 0; i < AllPlayers.Count; i++)
            {
                if (AllPlayers[i].GetPlayername() == username)
                {
                    DoesPlayerExists = true;
                    Locations = i;
                }
            }
            if (DoesPlayerExists)
            {
                return Locations;
            }
            else
            {
                AllPlayers.Add(new Player(username));
                return AllPlayers.Count - 1;
            }
        }

        private string GetAllItems()
        {
            string data = "";

            for (int i = 0; i < CheckUpList.GetAllItemsList().Count; i++)
            {
                data += CheckUpList.GetAllItemsList()[i].Getname() + "\n";
            }
            return data;
        }

        private string ConnectWords(List<string> list)
        {
            string returndata;

            returndata = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    returndata += " ";
                    returndata += list[i];
                }
            }

            return returndata;
        }

        private string StackItem(List<BaseItem> items)
        {
            string output = "";
            bool exists = false;
            List<CountItems> stacks = new List<CountItems>();

            foreach (BaseItem element in items)
            {
                if (stacks.Count == 0)
                {
                    stacks.Add(new CountItems(element));
                }
                else
                {
                    exists = false;
                    foreach (CountItems things in stacks)
                    {
                        if (element.Getname() == things.GetItem().Getname())
                        {
                            things.AddMoreItem();
                            exists = true;
                        }
                    }

                    if (exists == false)
                    {
                        stacks.Add(new CountItems(element));
                    }
                }
            }

            foreach (CountItems element in stacks)
            {
                if (element.GetAmount() > 0)
                {
                    output += element.GetItem().Getname() + " (x" + element.GetAmount() + ")\n";
                }
                else
                {
                    output += element.GetAmount() + "\n";
                }
            }

            return output;
        }

        class CountItems
        {
            private int amount;
            private BaseItem item;

            public CountItems(BaseItem InputItem)
            {
                amount = 1;
                item = InputItem;
            }

            public void AddMoreItem()
            {
                amount++;
            }

            public BaseItem GetItem()
            {
                return item;
            }

            public int GetAmount()
            {
                return amount;
            }
        }
    }
}