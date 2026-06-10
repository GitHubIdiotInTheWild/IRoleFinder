using BepInEx;
using BepInEx.Unity.IL2CPP;
using UnityEngine;
using AmongUs.GameOptions;
using Il2CppInterop.Runtime.Injection;

[BepInPlugin("com.hp.impostorrhud", "ImpostorHUD", "1.0.0")]
public class ImpostorHUDPlugin : BasePlugin {
    public override void Load() {
        ClassInjector.RegisterTypeInIl2Cpp<HUDComponent>();
        AddComponent<HUDComponent>();
    }
}

public class HUDComponent : MonoBehaviour {
    private Texture2D? bgTexture;
    private Font? vcrFont;

    void OnGUI() {
        if (bgTexture == null) {
            bgTexture = new Texture2D(1, 1);
            bgTexture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.75f));
            bgTexture.Apply();
            vcrFont = Font.CreateDynamicFontFromOSFont("VCR OSD Mono", 14);
        }

        if (GameData.Instance == null) return;

        int evilCount = 0;
        foreach (var p in GameData.Instance.AllPlayers) {
            if (p == null || p.Role == null) continue;
            var r = p.Role.Role;
            if (r == RoleTypes.Impostor || r == RoleTypes.Shapeshifter
             || r == RoleTypes.Phantom || r == RoleTypes.Viper) evilCount++;
        }

        if (evilCount == 0) return;

        float panelW = 220f;
        float headerH = 30f;
        float rowH = 26f;
        float padding = 8f;
        float panelH = headerH + (evilCount * rowH) + padding;
        float x = 1366f - panelW - 10f;
        float y = 110f;

        GUI.Box(new Rect(x, y, panelW, panelH), "");

        GUIStyle headerStyle = new GUIStyle();
        headerStyle.normal.textColor = Color.white;
        headerStyle.fontSize = 14;
        headerStyle.fontStyle = FontStyle.Bold;
        headerStyle.alignment = TextAnchor.MiddleCenter;
        headerStyle.font = vcrFont;
        GUI.Label(new Rect(x, y, panelW, headerH), "⚠ EVIL ROLES", headerStyle);

        GUIStyle nameStyle = new GUIStyle();
        nameStyle.fontSize = 13;
        nameStyle.fontStyle = FontStyle.Bold;
        nameStyle.alignment = TextAnchor.MiddleLeft;
        nameStyle.font = vcrFont;

        float rowY = y + headerH + 4f;
        foreach (var p in GameData.Instance.AllPlayers) {
            if (p == null || p.Role == null) continue;
            var role = p.Role.Role;

            if (role == RoleTypes.Impostor) nameStyle.normal.textColor = new Color(1f, 0.3f, 0.3f);
            else if (role == RoleTypes.Shapeshifter) nameStyle.normal.textColor = new Color(1f, 0.5f, 0f);
            else if (role == RoleTypes.Phantom) nameStyle.normal.textColor = new Color(0.7f, 0.3f, 1f);
            else if (role == RoleTypes.Viper) nameStyle.normal.textColor = new Color(0.2f, 0.9f, 0.2f);
            else continue;

            GUI.Label(new Rect(x + 10f, rowY, panelW - 10f, rowH),
                p.PlayerName + "  —  " + role.ToString(), nameStyle);
            rowY += rowH;
        }
    }
}