using System;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker.Actions;
using Modding;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace Bullshittery
{
    public class Bullshittery : Mod
    {
        public override List<(string, string)> GetPreloadNames()
        {
            return new List<(string, string)>
            {
                ("Deepnest_East_11", "Super Spitter")
            };
        }
        public static GameObject PrimalAspid;
        

        internal static Bullshittery Instance;

        public Bullshittery() : base("Bullshittery") { }
        public override string GetVersion() => "1.4.69.2"; // Major.Minor.Build.Revision
        public bool greenEnabled = true;
        public bool redEnabled = false;
        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Log("Initializing");

            Instance = this;

            Log("Initialized");

            GameObject PrimalAspidPrefab = preloadedObjects["Deepnest_East_11"]["Super Spitter"];
            UObject.DontDestroyOnLoad(PrimalAspidPrefab);
            PrimalAspid = PrimalAspidPrefab;
            PrimalAspid.SetActive(false);

            
            


            
            ModHooks.HeroUpdateHook += () =>
            {
                HeroController.instance.GetComponent<tk2dSprite>().color = Color.green;
            };
            ModHooks.SavegameSaveHook += SaveHook;
            ModHooks.HeroUpdateHook += PrimalAspidTimer;
            ModHooks.HeroUpdateHook += SoulHPTimer;

        }

        

        private void SaveHook(int obj)
        {
            HeroController hero = HeroController.instance;
            if (hero == null) { return; }
            int chance = UnityEngine.Random.Range(1, 1000);
            if (chance == 1)
            {
                hero.TakeDamage(hero.gameObject, GlobalEnums.CollisionSide.other, 9999, 0);
                Log("bro got unlucky 💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀");
            }
        }



        private float aspidTimer = 0f;
        private const float aspidTimerDuration = 40f;
        private void PrimalAspidTimer()
        {
            HeroController hero = HeroController.instance;

            aspidTimer += Time.deltaTime;
            if (aspidTimer >= aspidTimerDuration)
            {
                aspidTimer -= aspidTimerDuration;

                Vector3 knightPos = hero.gameObject.GetComponent<Transform>().position;

                Vector3 aspidSpawn1 = knightPos + new Vector3(-2f, 2f, knightPos.z);
                Vector3 aspidSpawn2 = knightPos + new Vector3(2f, 2f, knightPos.z);

                GameObject Aspid1 = UObject.Instantiate(PrimalAspid);
                GameObject Aspid2 = UObject.Instantiate(PrimalAspid);

                Aspid1.GetComponent<Transform>().position = aspidSpawn1;
                Aspid2.GetComponent<Transform>().position = aspidSpawn2;
                Aspid1.SetActive(true);
                Aspid2.SetActive(true);
            }
        }
        
        private void SpawnPrimalAspid(Vector3 position)
        {
            GameObject Aspid = UObject.Instantiate(PrimalAspid);

            Aspid.GetComponent<Transform>().position = position;
            Aspid.SetActive(true);
        }

        private float soulHPTimer = 0f;
        private const float soulHPTimerDuration = 60f;
        private void SoulHPTimer()
        {
            HeroController hero = HeroController.instance;
            PlayerData pd = hero.playerData;
            soulHPTimer += Time.deltaTime;

            Vector3 knightPos = hero.GetComponent<Transform>().position;
            if (soulHPTimer >= soulHPTimerDuration)
            {
                soulHPTimer -= soulHPTimerDuration;
                if (pd.MPCharge == 0)
                    Log("Keep your soul meter filled dingus");
                Vector3 aspidSpawn1 = knightPos + new Vector3(-4f, 2f, knightPos.z);
                Vector3 aspidSpawn2 = knightPos + new Vector3(4f, 2f, knightPos.z);
                Vector3 aspidSpawn3 = knightPos + new Vector3(0f, 4f, knightPos.z);
                int i = 0;
               while (i < 3) 
               {
                    i++;
                    SpawnPrimalAspid(aspidSpawn1);
                    SpawnPrimalAspid(aspidSpawn2);
                    SpawnPrimalAspid(aspidSpawn3);
               }
                
            }

            }
        }
}