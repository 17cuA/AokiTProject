﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroups_01 : MonoBehaviour
{
	public EnemyCreate.EnemyGroup[] EnemyGroups = new EnemyCreate.EnemyGroup[101]
	{
		new EnemyCreate.EnemyGroup("None", EnemyCreate.EnemyType.NONE, EnemyCreate.CreatePos.L0, false, 120),
		new EnemyCreate.EnemyGroup("円盤上10", EnemyCreate.EnemyType.UFO_GROUP_NONESHOT, EnemyCreate.CreatePos.R3, true, 240),
		new EnemyCreate.EnemyGroup("円盤下10", EnemyCreate.EnemyType.UFO_GROUP_NONESHOT, EnemyCreate.CreatePos.Rm3, true, 300),
		new EnemyCreate.EnemyGroup("闘牛斜め配置中央アイテム", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_FOUR, EnemyCreate.CreatePos.L0, true, 390),
		new EnemyCreate.EnemyGroup("円盤上", EnemyCreate.EnemyType.UFO_GROUP_NONESHOT, EnemyCreate.CreatePos.R3, true, 0),
		new EnemyCreate.EnemyGroup("円盤下", EnemyCreate.EnemyType.UFO_GROUP_NONESHOT, EnemyCreate.CreatePos.Rm3, true, 240),
		new EnemyCreate.EnemyGroup("円盤上狭", EnemyCreate.EnemyType.UFO_GROUP_NONESHOT, EnemyCreate.CreatePos.R1, true, 0),
		new EnemyCreate.EnemyGroup("円盤下狭", EnemyCreate.EnemyType.UFO_GROUP_NONESHOT, EnemyCreate.CreatePos.Rm1, true, 240),
		new EnemyCreate.EnemyGroup("闘牛突進三角B", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_FOURTRIANGLE_B, EnemyCreate.CreatePos.L0, true, 240),
		new EnemyCreate.EnemyGroup("闘牛突進三角C", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_FOURTRIANGLE_C, EnemyCreate.CreatePos.L0, false, 240),
		new EnemyCreate.EnemyGroup("闘牛縦3アイテム", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_THREE, EnemyCreate.CreatePos.L0, true, 45),
		new EnemyCreate.EnemyGroup("闘牛上2下2", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TOPANDUNEDR, EnemyCreate.CreatePos.FOURGROUPL, false, 45),
		new EnemyCreate.EnemyGroup("闘牛縦3アイテム", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_THREE, EnemyCreate.CreatePos.L0, true, 45),
		new EnemyCreate.EnemyGroup("闘牛上2下2", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TOPANDUNEDR, EnemyCreate.CreatePos.FOURGROUPL, false, 45),
		new EnemyCreate.EnemyGroup("闘牛縦7", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_FIVE, EnemyCreate.CreatePos.FOURGROUPL, false, 45),
		new EnemyCreate.EnemyGroup("闘牛縦7", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_FIVE, EnemyCreate.CreatePos.FOURGROUPL, false, 45),
		new EnemyCreate.EnemyGroup("闘牛縦7", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_FIVE, EnemyCreate.CreatePos.FOURGROUPL, false, 360),
		new EnemyCreate.EnemyGroup("🔲🔲🔲ビッグコア🔲🔲🔲", EnemyCreate.EnemyType.BIGCORE, EnemyCreate.CreatePos.L0, false, 180),
		new EnemyCreate.EnemyGroup("闘牛上2", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN, EnemyCreate.CreatePos.R4, false, 0),
		new EnemyCreate.EnemyGroup("闘牛下2", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP, EnemyCreate.CreatePos.Rm4, false, 180),
		new EnemyCreate.EnemyGroup("闘牛上2", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN, EnemyCreate.CreatePos.R4, false, 0),
		new EnemyCreate.EnemyGroup("闘牛下2", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP, EnemyCreate.CreatePos.Rm4, false, 180),
		new EnemyCreate.EnemyGroup("闘牛上2", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN, EnemyCreate.CreatePos.R4, false, 0),
		new EnemyCreate.EnemyGroup("闘牛下2", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP, EnemyCreate.CreatePos.Rm4, false, 240),
		new EnemyCreate.EnemyGroup("闘牛上2", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN, EnemyCreate.CreatePos.R4, false, 0),
		new EnemyCreate.EnemyGroup("闘牛下2", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP, EnemyCreate.CreatePos.Rm4, false, 180),
		new EnemyCreate.EnemyGroup("闘牛上2", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN, EnemyCreate.CreatePos.R4, false, 0),
		new EnemyCreate.EnemyGroup("闘牛下2", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP, EnemyCreate.CreatePos.Rm4, false, 510),
		new EnemyCreate.EnemyGroup("ビッグコア後2", EnemyCreate.EnemyType.BIGCOREENDGROUP, EnemyCreate.CreatePos.L0, false, 0),
		new EnemyCreate.EnemyGroup("ハエ2", EnemyCreate.EnemyType.BEELZEBUB_GROUP_TWOWIDE, EnemyCreate.CreatePos.R0, true, 270),
		new EnemyCreate.EnemyGroup("ビートル3", EnemyCreate.EnemyType.BEETLE_GROUP_THREE, EnemyCreate.CreatePos.L0, false, 300),
		new EnemyCreate.EnemyGroup("円盤上射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.R3, true, 0),
		new EnemyCreate.EnemyGroup("円盤下射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.Rm3, true, 360),
		new EnemyCreate.EnemyGroup("戦艦上下(現在停止中)", EnemyCreate.EnemyType.NONE, EnemyCreate.CreatePos.L0, false, 0),
		new EnemyCreate.EnemyGroup("戦艦", EnemyCreate.EnemyType.BATTLESHIP, EnemyCreate.CreatePos.R0, false, 0),
		new EnemyCreate.EnemyGroup("闘牛10直進上", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, EnemyCreate.CreatePos.R3, true, 0),
		new EnemyCreate.EnemyGroup("闘牛10直進下", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, EnemyCreate.CreatePos.Rm3, true, 180),
		new EnemyCreate.EnemyGroup("闘牛10直進上", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, EnemyCreate.CreatePos.R3, true, 0),
		new EnemyCreate.EnemyGroup("闘牛10直進下", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, EnemyCreate.CreatePos.Rm3, true, 90),
		new EnemyCreate.EnemyGroup("戦艦上下", EnemyCreate.EnemyType.BATTLESHIP_TOPANDUNDER, EnemyCreate.CreatePos.R0, false, 150),
		new EnemyCreate.EnemyGroup("闘牛縦3直進", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_THREESTRAIGHT, EnemyCreate.CreatePos.R0, false, 120),
		new EnemyCreate.EnemyGroup("闘牛縦3直進", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_THREESTRAIGHT, EnemyCreate.CreatePos.R0, false, 120),
		new EnemyCreate.EnemyGroup("闘牛縦3直進", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_THREESTRAIGHT, EnemyCreate.CreatePos.R0, false, 210),
		new EnemyCreate.EnemyGroup("闘牛縦7", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_SEVEN, EnemyCreate.CreatePos.R0, true, 45),
		new EnemyCreate.EnemyGroup("闘牛縦7", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_SEVEN, EnemyCreate.CreatePos.R0, true, 180),
		new EnemyCreate.EnemyGroup("🔲🔲🔲🔲🔲ビッグコアマーク2🔲🔲🔲🔲🔲", EnemyCreate.EnemyType.BIGCOREMK2, EnemyCreate.CreatePos.L0, false, 120),
		new EnemyCreate.EnemyGroup("ヒトデ12", EnemyCreate.EnemyType.STARFISH, EnemyCreate.CreatePos.L0, true, 600),
		new EnemyCreate.EnemyGroup("隕石20", EnemyCreate.EnemyType.BOUNDMETEORS, EnemyCreate.CreatePos.L0, false, 210),
		new EnemyCreate.EnemyGroup("バキュラ6", EnemyCreate.EnemyType.BACULA_GROUP_SIX, EnemyCreate.CreatePos.R0, false, 360),
		new EnemyCreate.EnemyGroup("隕石20", EnemyCreate.EnemyType.BOUNDMETEORS, EnemyCreate.CreatePos.L0, false, 300),
		new EnemyCreate.EnemyGroup("隕石20", EnemyCreate.EnemyType.BOUNDMETEORS, EnemyCreate.CreatePos.L0, false, 270),
		new EnemyCreate.EnemyGroup("隕石20", EnemyCreate.EnemyType.BOUNDMETEORS, EnemyCreate.CreatePos.L0, false, 270),
		new EnemyCreate.EnemyGroup("ヒトデ12", EnemyCreate.EnemyType.STARFISH, EnemyCreate.CreatePos.L0, true, 600),
		new EnemyCreate.EnemyGroup("円盤上10狭射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.R1, true, 0),
		new EnemyCreate.EnemyGroup("円盤下10射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.Rm3, true, 75),
		new EnemyCreate.EnemyGroup("円盤上10射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.R3, true, 0),
		new EnemyCreate.EnemyGroup("円盤下10狭射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.Rm1, true, 75),
		new EnemyCreate.EnemyGroup("円盤上10狭射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.R1, true, 0),
		new EnemyCreate.EnemyGroup("円盤下10射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.Rm3, true, 75),
		new EnemyCreate.EnemyGroup("円盤上10射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.R3, true, 0),
		new EnemyCreate.EnemyGroup("円盤下10狭射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.Rm1, true, 120),
		new EnemyCreate.EnemyGroup("戦艦", EnemyCreate.EnemyType.BATTLESHIP, EnemyCreate.CreatePos.R0, false, 210),
		new EnemyCreate.EnemyGroup("円盤上10射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.R4, true, 0),
		new EnemyCreate.EnemyGroup("円盤下10射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.Rm4, true, 120),
		new EnemyCreate.EnemyGroup("戦艦", EnemyCreate.EnemyType.BATTLESHIP, EnemyCreate.CreatePos.R0, false, 0),
		new EnemyCreate.EnemyGroup("円盤上10射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.R4, true, 0),
		new EnemyCreate.EnemyGroup("円盤下10射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.Rm4, true, 120),
		new EnemyCreate.EnemyGroup("円盤上10射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.R4, true, 0),
		new EnemyCreate.EnemyGroup("円盤下10射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.Rm4, true, 120),
		new EnemyCreate.EnemyGroup("円盤上10射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.R4, true, 0),
		new EnemyCreate.EnemyGroup("円盤下10射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.Rm4, true, 420),
		new EnemyCreate.EnemyGroup("バキュラ2", EnemyCreate.EnemyType.BACULA_GROUP_TWO, EnemyCreate.CreatePos.R0, false, 120),
		new EnemyCreate.EnemyGroup("ハエ8", EnemyCreate.EnemyType.BEELZEBUB_GROUP_EIGHTNORMAL, EnemyCreate.CreatePos.R0, false, 180),
		new EnemyCreate.EnemyGroup("バキュラ2", EnemyCreate.EnemyType.BACULA_GROUP_TWO, EnemyCreate.CreatePos.R0, false, 270),
		new EnemyCreate.EnemyGroup("闘牛左上斜め配置7射撃", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_UPSEVENDIAGONAL, EnemyCreate.CreatePos.L0, false, 40),
		new EnemyCreate.EnemyGroup("闘牛左下斜め配置7射撃", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_DOWNSEVENDIAGONAL, EnemyCreate.CreatePos.L0, false, 40),
		new EnemyCreate.EnemyGroup("闘牛左上斜め配置7射撃", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_UPSEVENDIAGONAL, EnemyCreate.CreatePos.L0, false, 40),
		new EnemyCreate.EnemyGroup("闘牛左下斜め配置7射撃", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_DOWNSEVENDIAGONAL, EnemyCreate.CreatePos.L0, false, 40),
		new EnemyCreate.EnemyGroup("闘牛左上斜め配置7射撃", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_UPSEVENDIAGONAL, EnemyCreate.CreatePos.L0, false, 40),
		new EnemyCreate.EnemyGroup("闘牛左下斜め配置7射撃", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_DOWNSEVENDIAGONAL, EnemyCreate.CreatePos.L0, false, 15),
		new EnemyCreate.EnemyGroup("戦艦上下", EnemyCreate.EnemyType.BATTLESHIP_TOPANDUNDER, EnemyCreate.CreatePos.L0, false, 270),
		new EnemyCreate.EnemyGroup("闘牛10", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, EnemyCreate.CreatePos.R0, false, 0),
		new EnemyCreate.EnemyGroup("闘牛10", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, EnemyCreate.CreatePos.R0PX2Y081, false, 0),
		new EnemyCreate.EnemyGroup("闘牛10", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, EnemyCreate.CreatePos.R0PX2MY081, false, 115),
		new EnemyCreate.EnemyGroup("闘牛10", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, EnemyCreate.CreatePos.R0, false, 0),
		new EnemyCreate.EnemyGroup("闘牛10", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, EnemyCreate.CreatePos.R0PX2Y081, false, 0),
		new EnemyCreate.EnemyGroup("闘牛10", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, EnemyCreate.CreatePos.R0PX2MY081, false, 115),
		new EnemyCreate.EnemyGroup("闘牛10", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, EnemyCreate.CreatePos.R0, false, 0),
		new EnemyCreate.EnemyGroup("闘牛10", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, EnemyCreate.CreatePos.R0PX2Y081, false, 0),
		new EnemyCreate.EnemyGroup("闘牛10", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, EnemyCreate.CreatePos.R0PX2MY081, false, 115),
		new EnemyCreate.EnemyGroup("円盤上10狭射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.R1, true, 0),
		new EnemyCreate.EnemyGroup("円盤下10狭射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.Rm1, true, 90),
		new EnemyCreate.EnemyGroup("ビートル7", EnemyCreate.EnemyType.BEETLE_GROUP_SEVEN, EnemyCreate.CreatePos.R0, false, 15),
		new EnemyCreate.EnemyGroup("闘牛縦4突進", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_FOURVERTICALATTACK, EnemyCreate.CreatePos.R0, false, 45),
		new EnemyCreate.EnemyGroup("円盤上10射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.R4, true, 0),
		new EnemyCreate.EnemyGroup("円盤下10射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.Rm4, true, 60),
		new EnemyCreate.EnemyGroup("闘牛縦4突進", EnemyCreate.EnemyType.CLAMCHOWDER_GROUP_FOURVERTICALATTACK, EnemyCreate.CreatePos.R0, false, 60),
		new EnemyCreate.EnemyGroup("円盤上10射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.R3, true, 0),
		new EnemyCreate.EnemyGroup("円盤下10射撃", EnemyCreate.EnemyType.UFO_GROUP, EnemyCreate.CreatePos.Rm3, true, 360),
		new EnemyCreate.EnemyGroup("🔲🔲🔲🔲🔲ビッグコアマーク3🔲🔲🔲🔲🔲", EnemyCreate.EnemyType.BIGCOREMK3, EnemyCreate.CreatePos.L0, false, 120),
		new EnemyCreate.EnemyGroup("ゲームクリア", EnemyCreate.EnemyType.GAMECLEAR, EnemyCreate.CreatePos.L0, false, 10000),
	};
}
