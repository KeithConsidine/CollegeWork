# Git Commands List

## Git Version
```sh
git --version
```
Displays the installed Git version.

## Git Init
```sh
git init
```
Initializes a new Git repository in the current directory.

## Git Clone
```sh
git clone <repository_url>
```
Creates a copy of a remote repository locally.

## Git Status
```sh
git status
```
Shows the current status of the working directory and staging area.

## Git Add
```sh
git add <file>
```
Stages a file for commit.
```sh
git add .
```
Stages all changes in the current directory.

## Git Commit
```sh
git commit -m "commit message"
```
Commits the staged changes with a message.

## Git Branch
```sh
git branch
```
Lists all branches in the repository.
```sh
git branch <branch_name>
```
Creates a new branch.

## Git Checkout
```sh
git checkout <branch_name>
```
Switches to the specified branch.
```sh
git checkout -b <branch_name>
```
Creates and switches to a new branch.

## Git Merge
```sh
git merge <branch_name>
```
Merges the specified branch into the current branch.

## Git Pull
```sh
git pull
```
Fetches and integrates changes from a remote repository.

## Git Push
```sh
git push
```
Uploads local commits to a remote repository.
```sh
git push -u origin <branch_name>
```
Pushes a branch to the remote repository and sets it as the upstream branch.

## Git Log
```sh
git log
```
Displays the commit history.

## Git Reset
```sh
git reset --hard <commit_hash>
```
Resets the working directory and index to a specific commit, discarding changes.

## Git Stash
```sh
git stash
```
Temporarily saves changes that are not ready to be committed.
```sh
git stash pop
```
Applies the most recent stash and removes it from the stash list.

## Git Rebase
```sh
git rebase <branch_name>
```
Applies commits from one branch on top of another.

## Git Remote
```sh
git remote -v
```
Lists remote repositories associated with the local repository.

## Git Tag
```sh
git tag <tag_name>
```
Creates a new tag.
```sh
git tag
```
Lists all tags in the repository.

## Git Config
```sh
git config --global user.name "Your Name"
git config --global user.email "your.email@example.com"
```
Sets the global username and email for Git.

## Git Diff
```sh
git diff
```
Shows changes between the working directory and the staging area.
```sh
git diff <commit_hash>
```
Shows differences between a commit and the current state.
