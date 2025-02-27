# Sparta_Archero 


유니티 2022.3.17f1


# 12조 팀원 및 맡은 역할


김동호 : 플레이어 캐릭터, 스킬, 커스터마이징(색 변경), 사운드


김영헌 : 


류영재 : 


이성재 :


최승빈 :


 
## 타이틀 화면


![Image](https://github.com/user-attachments/assets/f36c15b0-7c4b-4aae-baa2-ea1e4df66e14)


게임 시작 시 표시되는 메인 로비 화면입니다.


UI는 전체적으로 FSM 디자인 패턴을 사용해 구현하였으며


UIManager클래스를 생성해 UIManager에서 편리하게 모든 UI를 조작 할 수 있도록 하였습니다.


Start 버튼을 누르게 되면 던전 로비로 이동하게 됩니다.


던전 로비에서는 현재 선택한 던전에 맞게 스프라이트를 변경 해주어 


플레이어가 현재 선택한 던전이 어느 던전인지 알기 쉽도록 하였습니다.


Setting 버튼을 누르게 되면 볼륨 설정창으로 이동하게 됩니다.


슬라이드바를 조절해 볼륨을 조절 할 수 있습니다.


사운드는 SoundManager 오브젝트를 통해 관리하며


AudioSource 컴포넌트를 여러개 생성해두어


효과음이 여러개 재생 될 수 있도록 하였습니다.



## 던전 입장

![Image](https://github.com/user-attachments/assets/7efe4681-697b-46b4-a49d-2daa1a30af79)


던전 선택 후 Start 버튼을 누르게 되면 던전으로 입장하게 됩니다.


던전 입장 시 플레이어를 생성해주며 


필요한 데이터(색상, 도전과제)를 PlayerPrefs를 통해 가져와 적용해줍니다.


Dungeon 1의 초반부는 기본적인 튜토리얼 맵으로 구성되어있습니다.


## 던전 내부

![Image](https://github.com/user-attachments/assets/e7b404ba-c5de-4df7-b231-55f48216d021)


플레이어는 방향키로 이동 할 수 있으며 이동 할 때 가장 가까운 몬스터를 찾고


멈췄을 때 가장 가까운 몬스터에게 화살을 발사하는 방식으로 작동하게 됩니다.


다음 스테이지로 넘어가는 콜라이더는 현재 필드에 적이 없을 시 활성화 됩니다.


플레이어가 다음 스테이지로 넘어가게 되면


StageManager를 통해 정해둔 위치로 이동시켜 씬 이동시에도


위치가 틀어지지 않도록 해주었습니다.


몬스터는 근접, 원거리, 보스 세 종류로 나뉘어져 있으며


근접 몬스터는 NavMesh3D를 2D에서 사용 할 수 있게 변환해둔 플러그인을 사용해


장애물이 있더라도 플레이어에게 도달 할 수 있도록 해주었습니다.


근접 몬스터와 플레이어의 콜라이더가 충돌하게 되면


플레이어에게 정해진 양의 데미지를 입히도록 하였습니다.


원거리 몬스터는 플레이어와 설정한 거리만큼 붙게 되면 원거리 공격을 합니다.


공격 시 투사체를 생성해 플레이어에게 이동하게 해주었고 


투사체와 플레이어의 콜라이더가 충돌하게 되면 


플레이어에게 정해진 양의 데미지를 입히도록 하였습니다.


몬스터 처치시 설정해둔 양의 경험치를 획득하게 되며 


레벨업 시 TimeScale을 0으로 설정 후 스킬 선택 UI가 나타납니다.


![녹화_2025_02_27_14_16_07_5](https://github.com/user-attachments/assets/a4d77f69-f3bf-42ca-85ec-6d57b49683cc)


스킬 선택 시 함수를 통해 선택한 스킬을 적용시킵니다.


![녹화_2025_02_27_14_16_31_628](https://github.com/user-attachments/assets/073137a5-ea26-4076-b635-cc595fa1eed7)


5개의 스테이지를 클리어하게 되면 보물상자가 존재하는 방으로 이동됩니다.


보물상자는 플레이어와의 거리를 감지해


일정거리 이하라면 Open 애니메이션이 재생되도록 설정해주었습니다.


보물상자를 열게되면 스킬을 선택하게되는 UI가 나타나며


현재 보물상자를 여는 도전과제가 달성되지 않았다면


도전과제 달성 UI를 표시해주고 도전과제에 맞는 보상을 획득 할 수 있게 해주었습니다.



## 맵 장애물 생성
![녹화_2025_02_27_14_17_17_212](https://github.com/user-attachments/assets/4e19b40b-68f9-4ac1-9e5a-82c780e6db87)



첫 던전의 튜토리얼 스테이지인 5스테이지 이후부터인 6스테이지 부터는


장애물, 함정 등이 랜덤하게 배치되도록 해주었습니다.


장애물, 함정은 총 4가지 종류가 구현됐으며


맵에 벽을 생성한다던가, 맵에 구멍을 뚫어 지나가지 못하도록 하거나


가시 함정을 설치해 밟았을 시 플레이어가 데미지를 입게 해주었습니다.


추가로 레버 함정은 장애물 + 함정 기능을 하며 지나가지 못하지만 가까이 가게되면


플레이어가 데미지를 입게 됩니다.


장애물, 함정 생성 시 겹치지 않게 하기 위해 일정거리 이상 떨어진 곳에만


생성되도록 해주었습니다. 


## 보스전


![녹화_2025_02_27_14_17_32_919](https://github.com/user-attachments/assets/e4434a48-d823-4c33-9b7e-a7f1c7f66a50)



10스테이지 마다 보스전으로 입장하게 됩니다.


보스는 기본적으로 많은 체력을 가지고 있으며


분노모드, 무적, 충격파 스킬들을 사용하도록 하였습니다.


충격파 스킬의 경우 쿨타임을 설정해두어 일정시간마다 주변에 데미지를 주도록 하였습니다.


보스의 체력이 30% 이하로 떨어지게 되면 분노모드가 설정되며


분노모드일 때는 이동속도가 증가하여 플레이어를 따라잡기 쉽도록 해주었습니다.


보스의 체력이 20% 이하로 떨어지게 되면 무적패턴이 실행되며


보스가 3초간 데미지를 입지 않게 됩니다.


보스전을 클리어하게 되면 던전 입장 후 보스 처치까지의 시간을 표시해줍니다.



## 도전과제 보상, 색상 변경
![녹화_2025_02_27_14_17_54_981](https://github.com/user-attachments/assets/b1f41eda-2d5e-44ff-bb1a-042bcbd800f4)


보물상자 열기 도전과제를 성공하게 되면


던전로비에서 ChangeColor 버튼이 나타나도록 해주었습니다.


도전과제 달성 여부는 PlayerPrefs에 따로 저장해두어


게임을 다시 실행하더라도 유지되도록 해주었습니다.


ChangeColor 버튼을 누르게 되면


색상 변경 UI가 나타나게 됩니다.


슬라이드바를 통해 R, G, B값을 조절해주고


Apply버튼을 누르게되면


PlayerPrefs에 현재 Color값을 Hex코드 문자열로 변환 후 저장해줍니다.


저장 된 색은 던전 입장 시에 LoadColor를 통해 PlayerPrefs에서 불러와 적용해줍니다.


![녹화_2025_02_27_14_18_17_829](https://github.com/user-attachments/assets/d1f34fbb-abab-43d4-83ba-3c97100438f0)


던전 입장 시에 색깔이 적용된 모습입니다.


추가로 던전에서도 Setting 버튼을 통해 볼륨 조절을 할 수 있게 하였습니다.
