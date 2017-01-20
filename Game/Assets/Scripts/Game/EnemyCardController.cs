using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class EnemyCardController : MonoBehaviour, IDropHandler
    {
        CardManager cardData;

        void Start()
        {
            cardData = GetComponent< CardManager >();
        }

        public void OnDrop( PointerEventData eventData )
        {
            CardAttackController attackerController = eventData.pointerDrag.GetComponent< CardAttackController >();
            cardData.Health -= attackerController.cardData.Attack;
            attackerController.cardData.Health -= cardData.Attack;
        }
    }
}