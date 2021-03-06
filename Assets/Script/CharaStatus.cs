﻿using UnityEngine;
using System;
using System.Collections;
using Cradle;
using Cradle.Resource;

namespace Cradle {
	public class CharaStatus : MonoBehaviour, IEffectController {
		public GameObject lastAttackTarget = null;
		private ParticleSystem powerUpEffect;
		public CharaStatusController controller;
		
		public void OnEnable() {
			controller.SetEffectController (this);
		}

		
		void Start(){
			if(controller.IsPlayer(gameObject.tag)){
				FindEffectComponent();
			}
		}
		
		void Update(){
			
			if(controller.IsNPC(gameObject.tag)){
				return;
			}

			controller.DisablePowerBoost ();
			controller.PowerUp ();
		}

		//アイテム取得
		public void GetItem(DropItemController.ItemKind itemKind){
			try{
				controller.GetItem (itemKind);
			}catch(ArgumentException e){
				Debug.Log("SaveErrorLog : " + e);
				TextReadWriteManager write = new TextReadWriteManager();
				write.WriteTextFile(Application.dataPath + "/" + "ErrorLog_Cradle.txt", e.ToString());
			}

		}

		public void FindEffectComponent() {
			this.powerUpEffect = transform.Find ("PowerUpEffect").GetComponent<ParticleSystem> ();
		}
		
		public void PlayEffect() {
			this.powerUpEffect.Play ();
		}
		
		public void StopEffect() {
			this.powerUpEffect.Stop();
		}
		
		public float GetPower() {
			return controller.GetPower ();
		}

		public float GetHP(){
			return controller.GetHP ();	
		}

		public float SetHP(float hp){
			return controller.SetHP (hp);	
		}

		public float HealHP(float hp){
			return controller.HealHP (hp);
		}

		public float DamageHP(float hp){
			return controller.DamageHP (hp);
		}

		public bool IsPowerBoost() {
			return controller.IsPowerBoost ();
		}
		
		public bool isAttacking() {
			return controller.IsAttacking();
		}
		
		public bool SetAttacking(bool flg){
			return controller.SetAttacking (flg);	
		}
		
		public bool IsDead() {
			return controller.IsDied ();
		}
		
		public bool setDied(bool flg){
			return controller.SetDied (flg);
		}

		public GameObject SetLastAttackTarget(GameObject obj){
			return this.lastAttackTarget = obj;
		}

	}
}