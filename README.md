# VirtualTheremin

## 概要
Oculus Touch等を用いてバーチャル空間で演奏できるテルミンのシミュレータです。Unity5以上対応

## 利用規約
「大田マト」のクレジット表記（チャンネルまたは動画へのリンクもあると嬉しいです）をしていただければ、
商用・非商用に関わらず動画やゲームに利用していただいて構いません。  
ただし、アセット自体の再配布はご遠慮ください。  
また、本アセットは無保証であり、利用によって生じた損害等について当方は責任を負わないものとします。

大田マトチャンネル  
https://www.youtube.com/channel/UCY-rzz8F3xdJ9NS3Y5iAGjw

## 利用方法
1. VirtualTheremin.unitypackageをダウンロード（ファイル名をクリックしてから画面右のDownloadボタンを押してください）
2. ダウンロードしたunitypackageをインポート  
3. Theremin.prefabをシーンに設置  
4. インスペクタのvolumeHandとpitchHandに、左手と右手に対応するGameObject（例えばOculus Utilities for Unityを使用している場合はLeftHandAnchorとRightHandAnchor）をアタッチ

音量操作：volumeHandを上に上げるほど音量が大きくなります。本体より下の場合は無音になります。  
ピッチ操作：pitchHandとピッチアンテナの距離が小さいほど音が高くなります。  
音色：Theremin.prefabのAudioSourceにアタッチされているwavファイルを差し替えることで音色を変更できます。  

利用方法に関してご不明な点がございましたらお気軽にtwitterでご連絡ください。  
https://twitter.com/ootamato
