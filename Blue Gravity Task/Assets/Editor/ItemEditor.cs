using UnityEditor;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    const string headInfoText = "Head sprites must maintain the following order: \n - Face \n - Hood";
    const string torsoInfoText = "Torso sprites must maintain the following order: \n - Elbow R \n - Wrist R \n - Shoulder R \n - Torso \n - Elbow L \n - Wrist L \n - Shoulder L";
    const string legInfoText = "Leg sprites must maintain the following order: \n - Leg L \n - Boot L \n - Leg R \n - Boot R \n - Pelvis";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Item item = (Item)target;

        switch (item.ItemType)
        {
            case Item.TypesOfItem.Hood:

                EditorGUILayout.HelpBox(headInfoText, MessageType.Info);
                break;

            case Item.TypesOfItem.Armor:

                EditorGUILayout.HelpBox(torsoInfoText, MessageType.Info);
                break;

            case Item.TypesOfItem.Boots:

                EditorGUILayout.HelpBox(legInfoText, MessageType.Info);
                break;
        }
    }
}
