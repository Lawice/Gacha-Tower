﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TD.Runtime.Tower.Inventory;
using UnityEngine;

namespace TD.Runtime.Gacha {
    public class ScGachaCardsManager : MonoBehaviour {
        private static readonly int Show = Animator.StringToHash("Show");
        [SerializeField] List<ScGachaCard> _cards;
        [SerializeField] private Animator _cardParentAnimator;

        public bool IsAutoShow;
        
        public bool CanPull = true;
        public bool IsAllCardShowed;
        public bool IsCardShowed;
        public bool IsShowing;
        public bool IsUnShowing;

        public void SetAutoShow(bool toggle) {
            IsAutoShow = toggle;
        }

        private void Start() {
            _cardParentAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }

        private void Update() {
            IsAllCardShowed = _cards.All(card => card.IsShowed);
            IsCardShowed = _cards.Any(card => card.IsShowed);
            IsShowing = _cards.Any(card => card.IsShowing);
            IsUnShowing = _cards.Any(card => card.IsUnShowing);
            
            if (IsAllCardShowed && !IsShowing && !IsUnShowing) {
                CanPull = true;
            }
            
            if (IsAutoShow&& !IsAllCardShowed && !IsShowing && !IsUnShowing) {
                StartCoroutine(ShowAllCards());
            }
        }

        public void SetCards(List<StTowerCard> towerCards) {
            for(int u = 0; u < towerCards.Count; u++) {
                _cards[u].SetTower(towerCards[u]);
            }
        }
        
        public IEnumerator HideCards() {
            if (!IsAllCardShowed) yield break;

            foreach (ScGachaCard card in _cards) {
                card.IsShowed = false;
                card.UnShow();
            }
            
            foreach (ScGachaCard card in _cards) {
                while ( card.IsUnShowing) {
                    yield return null;
                }
            }
        }
        
        public void ShowCards() {
            _cardParentAnimator.SetTrigger(Show);
            CanPull = false;
        }
        
        IEnumerator ShowAllCards() {
            yield return new WaitForSeconds(0.5f);
            foreach (ScGachaCard card in _cards) {
                card.Show();
            }
            
        }
        
    }
}