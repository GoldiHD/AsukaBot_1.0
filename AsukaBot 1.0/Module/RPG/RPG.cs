using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using AsukaBot_1._0.Module.RPG.Logic;
using AsukaBot_1._0.Module.RPG.Logic.Items;
using AsukaBot_1._0.Module.RPG.Logic.Questing;

namespace AsukaBot_1._0.Module.Music.Logic
{
    public class RPG : ModuleBase<SocketCommandContext>
    {

        private Inventory CheckUpList = new Inventory();
        private static List<Player> AllPlayers;

        #region Items

        [Command("_inv")]
        public async Task InventoryCheck()
        {
            int temp = DoIExist(Context.User.Username);
            if (temp == -1)
            {
                await Context.Channel.SendMessageAsync("Your inventory was empty");
            }
            else
            {
                if (AllPlayers[temp].GetInventory().GetTheInventory().Count > 0)
                {
                    string printString = null;
                    printString = "Gold: " + AllPlayers[temp].GetInventory().GetGold() + "\n";
                    for (int i = 0; i < AllPlayers[temp].GetInventory().GetTheInventory().Count; i++)
                    {
                        printString += AllPlayers[temp].GetInventory().GetTheInventory()[i].Getname() + "\n";
                    }
                    await Context.Channel.SendMessageAsync(printString);
                }
                else
                {
                    await Context.Channel.SendMessageAsync("empty");
                }
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
            await Context.Channel.SendMessageAsync(@"https://docs.google.com/spreadsheets/d/1XEya_u-TRM1W5NMBb9N0wAg9mCRh51BxGFGLxSznJG8/edit?usp=sharing");
        }

        [Command("_iteminfo")]
        public async Task ItemInfo(string para1, string para2 = null, string para3 = null, string para4 = null)
        {
            string temp;
            if (para4 != null)
            {
                temp = para1 + " " + para2 + " " + para3 + " " + para4;
            }
            else if (para3 != null)
            {
                temp = para1 + " " + para2 + " " + para3;
            }

            else if (para2 != null)
            {
                temp = para1 + " " + para2;
            }
            else
            {
                temp = para1;
            }

            bool ItemExsists = false;
            string data = "";
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
                    catch (Exception d)
                    {
                        Console.WriteLine(d);
                    }
                    await Context.Channel.SendMessageAsync(data);
                }
            }

            if (ItemExsists == false)
            {
                await Context.Channel.SendMessageAsync("Item [" + temp + "] does not exists");
            }
        }

        [Command("_equip")]
        public async Task EquipGear(string para1, string para2 = null, string para3 = null, string para4 = null)
        {
            string temp;
            if (para4 != null)
            {
                temp = para1 + " " + para2 + " " + para3 + " " + para4;
            }
            else if (para3 != null)
            {
                temp = para1 + " " + para2 + " " + para3;
            }

            else if (para2 != null)
            {
                temp = para1 + " " + para2;
            }
            else
            {
                temp = para1;
            }
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
                            if (AllPlayers[user].GetInventory().GetinventoryItemByName(temp) is WeaponsItem)
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

                string temp2;
                if (para4 != null)
                {
                    temp2 = para1 + " " + para2 + " " + para3 + " " + para4;
                }
                else if (para3 != null)
                {
                    temp2 = para1 + " " + para2 + " " + para3;
                }

                else if (para2 != null)
                {
                    temp2 = para1 + " " + para2;
                }
                else
                {
                    temp2 = para1;
                }

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

        [Command("give")]
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
                    string temp2;
                    if (para4 != null)
                    {
                        temp2 = para1 + " " + para2 + " " + para3 + " " + para4;
                    }
                    else if (para3 != null)
                    {
                        temp2 = para1 + " " + para2 + " " + para3;
                    }

                    else if (para2 != null)
                    {
                        temp2 = para1 + " " + para2;
                    }
                    else
                    {
                        temp2 = para1;
                    }

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
                    builder.AddField("Enemy", AllPlayers[temp].GetQuestManager().GetCombatManager().GetEnemy().GetName());
                    await ReplyAsync("", false, builder.Build());
                }
                else
                {
                    await Context.Channel.SendMessageAsync("you need to be out of an adventure to enter another");
                }
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
                await Context.Channel.SendMessageAsync("Error, please try again");
            }
            else
            {
                Statsholder = AllPlayers[temp].GetStats();
                if (AllPlayers[temp].GetPlayerState() == PlayerStates.Rest)
                {
                    await Context.Channel.SendMessageAsync("You're not in combat and so can't attack anything");
                }
                else
                {
                    builder.WithTitle("Attack").WithColor(Color.Red);
                    builder.AddField(Context.User.Username, AllPlayers[temp].Attack());
                    builder.AddField(AllPlayers[temp].GetQuestManager().GetCombatManager().GetEnemy().GetName(), AllPlayers[temp].CounterAttack());
                    builder.AddField("Health", Statsholder.GetVitallity().GetMyHealth() + "/" + Statsholder.GetVitallity().GetMyMaxHealth());
                    if (AllPlayers[temp].GetQuestManager().GetCombatManager().GetEnemy().GetHP() >= 0)
                    {
                        builder.AddField(AllPlayers[temp].GetQuestManager().GetCombatManager().GetEnemy().GetName() + "'s health", AllPlayers[temp].GetQuestManager().GetCombatManager().GetEnemy().GetHP() + "/" + AllPlayers[temp].GetQuestManager().GetCombatManager().GetEnemy().GetMaxHP());
                    }
                    else
                    {
                        builder.AddField(AllPlayers[temp].GetQuestManager().GetCombatManager().GetEnemy().GetName() + "'s health", "0" + "/" + AllPlayers[temp].GetQuestManager().GetCombatManager().GetEnemy().GetMaxHP());
                    }
                    if (AllPlayers[temp].GetQuestManager().GetCombatManager().GetEnemy().GetHP() <= 0)
                    {
                        builder.AddField("Xp gain", AllPlayers[temp].GetQuestManager().GetCombatManager().GetEnemy().GetXP());
                        builder.AddField("Loot", AllPlayers[temp].GetQuestManager().GetCombatManager().GetEnemy().GetLoot(AllPlayers[temp]));
                        try
                        {
                            builder.AddField("Gold" ,AllPlayers[temp].GetQuestManager().GetCombatManager().GetEnemy().GetGold(AllPlayers[temp])).ToString();  
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        if (AllPlayers[temp].AddXP(AllPlayers[temp].GetQuestManager().GetCombatManager().GetEnemy().GetXP()))
                        {
                            builder.AddField("Level up", "");
                        }
                        switch (AllPlayers[temp].GetPlayerState())
                        {
                            case PlayerStates.Encounter:
                                AllPlayers[temp].SetPlayerStates(PlayerStates.Rest);
                                AllPlayers[temp].SetQuestManager(null);
                                break;

                            case PlayerStates.Dungeon:
                                Console.WriteLine("still need more work");
                                break;

                            case PlayerStates.Campaign:
                                Console.WriteLine("still need more work");
                                break;

                            case PlayerStates.CollabBoss:
                                Console.WriteLine("still need more work");
                                break;

                            case PlayerStates.Pvp:
                                Console.WriteLine("still need more work");
                                break;
                        }
                    }
                    await ReplyAsync("", false, builder.Build());
                }
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
                    await Context.Channel.SendMessageAsync("You're currently in combat \n You're in : " + AllPlayers[temp].GetPlayerState() + "\n-------------- \n    " + Context.User.Username + "\n    Health: " + AllPlayers[temp].GetStats().GetVitallity().GetMyHealth() + "/" + AllPlayers[temp].GetStats().GetVitallity().GetMyMaxHealth() + "\n    MP: " + AllPlayers[temp].GetStats().GetMagic().GetMana() + "/" + AllPlayers[temp].GetStats().GetMagic().GetMaxMana() + "\n-------------- \n    " + AllPlayers[temp].GetQuestManager().GetCombatManager().GetEnemy().GetName() + "\n    Health: " + AllPlayers[temp].GetQuestManager().GetCombatManager().GetEnemy().GetHP() + "/" + AllPlayers[temp].GetQuestManager().GetCombatManager().GetEnemy().GetMaxHP());
                }
            }
        }

        #endregion

        #region Stats Commands

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
                builder.AddInlineField("Power", Statsholder.GetPower().GetPowerLvl());
                builder.AddInlineField("Magic", Statsholder.GetMagic().GetMagicLvl());
                builder.AddInlineField("Dexterity", Statsholder.GetDexterity().GetDexterityLvl());
                builder.AddInlineField("Intellegenc", Statsholder.GetIntellegence().GetIntellegencLvl());
                builder.AddInlineField("Vitallity", Statsholder.GetVitallity().GetVitallityLvl());
                builder.AddInlineField("Luck", Statsholder.GetLuck().GetLuckLvl());

                await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
        }

        [Command("status")]
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

        #endregion

        #region Market
        [Command("CheckMarket")]
        public async Task CheckMarketItems(string itemtype = null)
        {
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
                int TempCounter = 0;

                for (int i = 0; i < Holder.Count; i++)
                {

                    if (TempCounter == 25)
                    {
                        await Context.Channel.SendMessageAsync("", false, builder.Build());
                        builder = new EmbedBuilder();
                        TempCounter = 0;
                        builder.WithTitle("Market");
                        builder.WithDescription("filter: None");
                    }
                    builder.AddField(Holder[i].Getname(), "Price: " + Holder[i].GetPrice().ToString());
                    TempCounter++;
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

                    default:
                        builder.WithDescription("no filter like that exists");
                        break;
                }

                try
                {
                    for (int i = 0; i < CheckUpList.GetAllItemsList().Count; i++)
                    {
                        if (CheckUpList.GetAllItemsList()[i].GetBuyableState() == true)
                        {
                            List<ItemType> Temp = CheckUpList.GetAllItemsList()[i].GetItemType();

                            for (int x = 0; i < Temp.Count; x++)
                            {
                                if (Temp[x] == PickedType)
                                {
                                    Holder.Add(CheckUpList.GetAllItemsList()[i]);
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }



        }

        public async Task BuyFromMarket(string itemtype = null)
        {

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
                return -1;
            }
            else
            {
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

    }
}