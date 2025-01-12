using TD.Runtime.Tools;
using TD.Runtime.Tower;
using TD.Runtime.Tower.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TD.Runtime.Gacha {
    public class ScGachaCard : MonoBehaviour, IPointerClickHandler {
        private static readonly int ShowTrigger = Animator.StringToHash("Show");
        private static readonly int UnShowTrigger = Animator.StringToHash("UnShow");
        private Animator _animator;
        public bool IsShowing;
        public bool IsUnShowing;

        public StTowerCard Tower;
        private MeshRenderer _meshRenderer;
        private MeshRenderer _childMeshRenderer;

        public bool IsShowed;
        
        private void Awake() {
            _animator = GetComponent<Animator>();
            _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            _meshRenderer = GetComponent<MeshRenderer>();
            _childMeshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        }
        
        
        public void OnPointerClick(PointerEventData eventData) {
            Show();
        }

        public void Show() {
            if(IsShowed) return;
            _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            _animator.SetTrigger(ShowTrigger);
            IsShowed = true;
            IsShowing = true;
            ScGachaFX.Instance.PlayRarityFX(Tower.Rarity, transform.position + new Vector3(0, 1.5f, -1f));
            ScTowerManager.Instance.AddCard(Tower.Tower, Tower.Rarity, Tower.Fragments);
        }

        public void UnShow(){
            IsUnShowing = true;
            _animator.SetTrigger(UnShowTrigger);
        }
        
        public void OnShowEnd() {
            IsShowing = false;
        }
        public void OnUnShowEnd() {
            IsUnShowing = false;
        }
        
        public void SetTower(StTowerCard tower) {
            Tower = tower;
            Material[] materials = _meshRenderer.materials;
            Material[] materials2 = _childMeshRenderer.materials;

            if (materials.Length > 0) {
                materials[0] = Resources.Load<Material>("Materials/Card/Girls/" + tower.Tower.Name);
                _meshRenderer.materials = materials;

            }
            
            if (materials2.Length > 0) {
                materials2[0] = Resources.Load<Material>("Materials/Card/Rarity/" + tower.Rarity);
                _childMeshRenderer.materials = materials2;
            }
        
        }
        
    }
}