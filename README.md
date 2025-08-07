 (cd "$(git rev-parse --show-toplevel)" && git apply --3way <<'EOF' 
diff --git a//dev/null b/README.md
index 0000000000000000000000000000000000000000..762152492f22983e5a1a83446876107726451c29 100644
--- a//dev/null
+++ b/README.md
@@ -0,0 +1,52 @@
+# Vampire
+
+Unity 기반의 2D 액션 게임 프로젝트입니다. 본 문서는 프로젝트의 전체 구조와 **Assets/Scripts** 폴더에 포함된 주요 스크립트를 설명합니다.
+
+## 프로젝트 구조
+
+```
+Vampire/
+├── Assets/
+│   ├── Scripts/
+│   │   ├── Datas/
+│   │   ├── Game/
+│   │   ├── UI/
+│   │   └── Utility/
+│   └── ...
+├── Packages/
+└── ProjectSettings/
+```
+
+## Scripts
+
+### Datas
+- **ItemData.cs**: 아이템 정보를 담는 `ScriptableObject`. 아이템 유형, 기본 능력치, 레벨별 수치 및 투사체/아이콘을 정의합니다.
+
+### Game
+- **AchiveManager.cs**: 업적을 관리하고, 조건 충족 시 캐릭터 잠금을 해제합니다.
+- **AudioManager.cs**: BGM 및 효과음 재생을 담당하는 싱글턴 매니저.
+- **Bullet.cs**: 투사체의 데미지 및 관통 처리, 이동과 비활성화를 관리합니다.
+- **Character.cs**: 플레이어 캐릭터의 속도 배율 등 공통 속성을 제공합니다.
+- **Enemy.cs**: 적의 이동, 피격 및 사망 처리 로직을 포함합니다.
+- **Follow.cs**: 특정 UI 요소가 플레이어를 따라다니도록 화면 위치를 갱신합니다.
+- **GameManager.cs**: 게임의 전반적인 상태(시간, 경험치, 승리/패배 등)를 제어하는 중앙 매니저.
+- **Gear.cs**: 장비(글러브, 슈즈 등)를 통해 플레이어 또는 무기 능력을 보정합니다.
+- **Hand.cs**: 플레이어 손 스프라이트의 방향과 위치를 조정합니다.
+- **Player.cs**: 입력을 받아 플레이어를 이동시키고, 충돌 시 체력 감소와 사망 처리를 수행합니다.
+- **PoolManager.cs**: 각종 오브젝트를 풀링하여 생성/비활성화 비용을 절감합니다.
+- **Repostion.cs**: 카메라 영역을 벗어난 지형/적을 재배치합니다.
+- **Scanner.cs**: 주변 적을 탐지하고 가장 가까운 대상을 찾습니다.
+- **Spawner.cs**: 일정 간격으로 적을 생성하며, `SpawnData` 구조체로 속성을 정의합니다.
+- **Weapon.cs**: 무기의 회전/발사, 레벨 업 및 총알 생성 로직을 담당합니다.
+
+### UI
+- **GameResult.cs**: 승리/패배 결과 화면을 표시합니다.
+- **HUD.cs**: 경험치, 레벨, 처치 수, 남은 시간, 체력 등 HUD 정보를 갱신합니다.
+- **Item.cs**: 레벨업 시 선택 가능한 아이템 UI와 업그레이드 로직을 관리합니다.
+- **LevelUp.cs**: 레벨업 UI를 표시하고 무작위 아이템을 선택합니다.
 
EOF
)
