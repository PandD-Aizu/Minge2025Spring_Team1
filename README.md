# Minge2025Spring_Team1

# コーディング規則 他
## コーディング規則

### 定数
```C#
readonly int UPPER_SNAKE_CASE;
const int UPPER_SNAKE_CASE;
static readonly int UPPER_SNAKE_CASE;
```

### Enum
```C#
public enum UpperCamelCase
{
    UPPER_SNAKE_CASE
}
```

### クラス名
```C#
public class UpperCamelCase
{

};
```

### メンバ変数名
```C#
private int lowerCamelCase;
```

### メンバ関数名
```C#
@brief 関数の説明
@param 引数の説明(この行は必要に応じて書く)
@return 返り値の説明(この行は必要に応じて書く)
public void UpperCamelCase()
{
    
}
```

※
正直、見やすければ好きなように書いてかまいません。
他人が見ても理解できるように書いてほしいです🙇‍♀️

## ブランチ命名規則
### 機能実装
feature/(自身のGitHubID)/#(issue番号)/(issueのタイトル - AddTitleなど)

例)

feature/koto/#1/AddTitle

### バグ修正など
fix/(自身のGitHubID)/#(issue番号)/(issueのタイトル - FixTitleなど)

例)

fix/koto/#2/FixTitle

## issueの命名規則
### タイトル
[Feature or Fix] ここにタイトルを入力(EN or JP)

例)

[Feature] Add Title

[Fix] Fix Title

### 説明
適当な説明をつけておいてください

例)

タイトル画面を作成します

タイトル画面で画像が表示されないバグを修正します

## プルリクの命名規則
### タイトル
Feat or Fix: ここにタイトルを入力(EN or JP)

例)

Feat: Add Title

### 説明
何を実装したかを説明すればOKです。

例)

タイトル画面にボタンを配置しました。

ボタンをクリックするとシーンが切り替わります。
