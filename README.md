# Orbit

CLI ベースの軽量タスク管理ツール。
ターミナルから素早くタスクの作成・管理・進捗記録ができます。

## インストール

1. Releases から `obt` をダウンロード
2. 配置してパスを通す

```bash
mkdir -p ~/.local/bin
mv obt ~/.local/bin/
chmod +x ~/.local/bin/obt
```

`~/.local/bin` にパスが通っていない場合は、シェルの設定ファイルに追記:

```bash
# ~/.zshrc または ~/.bashrc に追加
export PATH="$HOME/.local/bin:$PATH"
```

3. 初期化

```bash
obt init
```

## 使い方

```bash
# タスクを作成
obt add "認証機能の実装"

# タスク一覧を表示
obt show

# タスクの詳細を表示
obt show 1

# 進捗を記録
obt p 1 "API仕様FIX"

# 説明を追記
obt desc 1 "OAuth2対応も含む"

# タスクを完了にする
obt done 1
```

### 表示例

```
$ obt show
  #1  [ ]  認証機能の実装
  #2  [x]  README整備
  #3  [ ]  CI構築

$ obt show 1
─── #1: 認証機能の実装 ──────────────
Status:  InProgress
Created: 2026-03-15

Description:
  OAuth2対応も含む

Progress:
  [2026-03-19 14:30] 設計完了、API仕様FIX
  [2026-03-20 10:15] レビュー待ち
```

## コマンドリファレンス

### 初期化・管理

| コマンド | 説明 |
|---|---|
| `obt init` | 初期化（`~/.orbit` を作成） |
| `obt clean` | `~/.orbit` を完全に削除 |

### タスク操作

| コマンド | 説明 |
|---|---|
| `obt add <title>` | タスクを作成 |
| `obt show` | 進行中のタスク一覧を表示 |
| `obt show -a` | 完了タスクも含めて一覧表示 |
| `obt show -v` | 最新の進捗ログ付きで一覧表示 |
| `obt show <id>` | タスクの詳細を表示 |
| `obt done <id>` | タスクを完了にする |
| `obt rename <id> "title"` | タスクのタイトルを変更 |
| `obt remove <id>` | タスクを削除 |

### 説明 (Description)

| コマンド | 説明 |
|---|---|
| `obt desc <id> "text"` | 説明を追記 |
| `obt desc <id>` | `$EDITOR` で説明を編集（未設定時は vi） |

### 進捗ログ (Progress)

| コマンド | 説明 |
|---|---|
| `obt progress <id> "message"` | 進捗ログを追加 |
| `obt p <id> "message"` | 同上（短縮形） |

### ワークスペース

| コマンド | 説明 |
|---|---|
| `obt ws` | 現在のワークスペースを表示 |
| `obt ws switch <name>` | ワークスペースを切り替え |
| `obt ws add <name>` | ワークスペースを作成 |
| `obt ws list` | ワークスペース一覧を表示 |
| `obt ws remove <name>` | ワークスペースを削除 |

## データ保存先

すべてのデータは `~/.orbit/` に保存されます。

```
~/.orbit/
├── config.json
└── workspaces/
    └── default/
        ├── workspace.json
        └── tasks/
            ├── 1.json
            └── 1.desc.md
```
