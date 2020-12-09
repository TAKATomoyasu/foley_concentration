# 大爆走！オーディオレーシング（SuperAudioRacing）

## 概要

制作展2020/7のために作ってるゲーム

## リンク

[Scrapbox](https://scrapbox.io/AudioRacing)  
[素材フォルダ](https://drive.google.com/drive/folders/1m-grsw5NYE_N3H7_ZUPnDMQJwM43mcHF?usp=sharing)

## メンバーと役職（決定責任者）

たかちん  
なぁちゃん  
開元さん  
  
プロデュース：たかちん  
ディレクション：たかちん  
プランニング：たかちん  
シナリオ：ななちゃん  
実況：開元さん  
グラフィック：たかちん  
サウンド：たかちん  
プログラマー：たかちん  
宣伝素材：(絵)開元さん、(清書、動画)たかちん  

## コンセプト

音だけでプレイする映像のないレーシングゲーム
プレイヤーは音からコースを想像し、車を走らせる
効果音、実況、音楽に集中し、脳内マップを作りながら走る

〜をするのがテーマ／目的  
プレイヤーは〜する  
〜が面白いポイント（こうするのが上手いプレイ）

## Unityプロジェクトの構成

SampleSceneのCarを使って制作。  
やがては自作の車にしたい。軽くなりそう。  
サウンドが重要なのでADX2を導入したい。  
スマホでもWebGLでプレイできるようにしたい。  
→JavaScriptで値を取る方法を採用する必要がある  
全体の設計にはUniRxを使ったObservableパターンを採用

### マネージャークラスを設置

- 各シーンManager｜シーン内の進行状況とイベントを管理(GameManager, TitleManagerなど)  
- UiManager｜Ui要素を全て管理  
- JsonDatabaseManager｜Jsonファイルによるデータの入出力を管理  
- SoundManager｜UI等の効果音を管理  
- MusicManager｜音楽を管理（シーンMusicとSingletonMusic）
- TransitionManager｜シーン切り替えを管理  

DontDestroy  
- SingletonManager｜DontDestroyなSingletonを管理（暗転とAudioSource）

### Managerクラスについて

GameManagerは進行状態を管理するだけ。  
GameManagerはシーン内で唯一のInstanceにする（Don't Destroyはしない）
Gameの進行状況はEnum Stateのインスタンスで保持される。（State.cs）
従って、GameManager.Instance.Stateでどのスクリプトからでも取得できる。

参照の方向がこれまでと変わって、
Managerが下位のクラスを把握しているというスタイルではなく、下位のクラスが上位のクラスを参照する設計にした。
また、その他のManagerクラスも、そこから.GetComponent<>()で取得できる

そして、stateはReactivePropertyとし、他のクラスはゲームの進行状況をSubscribeし、
状況が進展した場合に自発的にアクションを開始することで、非常に疎結合な設計にした！

### SingletonManagerについて

シーンをまたげるようにDon't DestroyしたSingletonオブジェクトにアタッチする。  
暗転時のSpriteとAudioSourceのみ管理する。  
また、SingletonのスクリプトとAudioSourceは全てSingletonManagerで管理するようにした。  
→Awake()内でSingletonのスクリプトをシーンからGetComponentしないため。

### メモ

Awake()内で、Singleton化しているため、
Awake()内で GameManager.Instance に他のクラスからアクセスすると、どちらが先か不明なため危険。

車の接触判定をCapsuleにしたいのに、ColliderBottomをオフにすると暴れだすの意味がわからん  
しかも、明らかにタイヤと接触してるのに、なんで平気なんだよ

## 制作上のTODO

基本的には、SOUND ONLYの表示をしとくのがいいかな？  
トランジション用の暗幕はSingleton、  
レース中の開閉できる暗幕は、シーンごとに用意したほうがよさそう。

SceneManagerという抽象クラスを作り、GameManagerやTitleManagerはそれを継承するのがよさそう  
ボイスManagerとかも、抽象クラス作れそう。ボイスは複数作れるようにして、単一なら1つだけ作ればいいし。
  
GameManager.Instance を、
SceneManager.Instance に変更して、抽象クラス側でプロパティを設定することってできるのかな
実況のセリフファイルをAudioClip[]ではなく、enumで管理するとよいかな？
→やってみたら、30要素ぐらいのenumになり、ぐちゃぐちゃ。接頭語で段階を分けたけど、分かりづらい。
→それよりも、AudioClip変数を分割する方がシンプルでよい

任天堂のSEシステムみたいに、タイミングやパラメーターが取得できる状態で、
効果音側がタイミングやパラメーターを文字で指定すれば動くような状態にできるとよいか？

基本的な思想として、Managerクラスにゲーム固有のシステムは組み込まない。
別のゲームにも、そのまま持っていって使える。
そしてManagerクラスはあらゆるシーンに存在する。Don't Destroyはしない。
各シーンに存在するシーン固有の機能を管理するクラスは、...Controllerなどと命名する

ひょっとして、いまは、各シーンごとにManagerを参照してるけど
Managerクラスを継承して今までの全部作ってればよかったとかあるかな？

トランジションのシーン名をEnumで管理するプラグインを追加したよん

抽象クラスを利用して、いい感じにしようと思ったけど、無理だったよ
他のクラスからアクセスする時にも引数に<T>を埋めないといけなくて、
それも一致しないと値が取得できないことを知って断念した。(´ •ω•`)
（てか、ジェネリック引数が一致しない場合には別のインスタンスとみなされるのかな）
→恐らく

なので、Managerクラスというマーカー的なものを用意して、それを使うことにした。
Find()は絶対に使いたくない！！( ˘•ω•˘ )

Snapshotは、もしかしたらシーン切替時に前のページでやったほうがよいかも知れない

## 学んだこと

## 疑問

## 制作記録

- 1/1 制作開始、テキスト表示開始・完了
- 6/11 
- 6/17 Gitでバックアップを取り始めた
- 6/18 謎エラーが止まらなくなったので、バックアップに戻した。ファイル階層を整理
        

