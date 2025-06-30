using UnityEngine;

public class InteractableStorableObject : InteractableObject
{
    #region Variables
    [Tooltip("TO BE CHANGED: Scriptable Object for the object to be handle in this interaction.")]
    [SerializeField] private GameObject objectProperties;
    [Tooltip("If 'Stored' the object will be stored in the player's inventory, meanwhile, if 'Removed' the object will be taken out of the player's inventory.")]
    [SerializeField] private ObjectPurpose objectPurpose = ObjectPurpose.Stored;
    public enum ObjectPurpose { Stored, Removed }

    public InteractableStorableObject()
    {
        AllowPostInteraction = false;
    }
    #endregion

    #region Methods

    public override void Interact()
    {
        base.Interact();

        if (objectProperties != null)
        {
            if (objectPurpose == ObjectPurpose.Stored)
            {
                StoreObjectToInventory();
            }
            else
            {
                RemoveObjectFromInventory();
            }
        }
    }

    private void StoreObjectToInventory()
    {
        SimulateStoring();
    }

    private void RemoveObjectFromInventory()
    {
        SimulateRemoving();
    }

    private void SimulateStoring()
    {
        Debug.Log("Object has bee added to the inventory.");
    }

    private void SimulateRemoving()
    {
        Debug.Log("Object has bee added to the inventory.");
    }
    #endregion
}
