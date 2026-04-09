# GitHub Copilot Instructions

## Commit and Write Operation Rules

**CRITICAL: NO AUTOMATIC COMMITS**

- **NEVER** create, push, or commit any changes without explicit user command
- **NEVER** automatically stage files or create commits
- **ALWAYS** require direct user permission before any write operation
- **ALWAYS** ask for confirmation before creating branches, pushing files, or modifying repository content
- **ALWAYS** present proposed changes for review before executing any git operations

### Copilot CLI Rules

When using GitHub Copilot CLI (`gh copilot`), you MUST:
- **DO NOT** execute git commit commands automatically
- **DO NOT** execute git push commands automatically
- **ALWAYS** explain what changes would be made and wait for user confirmation
- **ALWAYS** use `--dry-run` or preview mode when available

### Allowed Operations

You MAY:
- Suggest code changes and improvements
- Generate code snippets and examples
- Explain code and answer questions
- Provide file content recommendations
- Draft commit messages for user review

You MUST NOT:
- Execute `git commit` without explicit user instruction
- Execute `git push` without explicit user instruction
- Create or modify files in the repository without user approval
- Automatically stage changes with `git add`
- Merge branches or create pull requests without permission

### User Confirmation Required

Before ANY of these operations, you MUST:
1. Show exactly what will be changed
2. Explain the impact of the changes
3. Wait for explicit user approval (e.g., "yes, commit this" or "push these changes")
4. Confirm the user understands the operation

### Examples of Required Permission

❌ **NOT ALLOWED without permission:**
```bash
git commit -m "Fix bug"
git push origin main
gh copilot suggest -t shell "commit all changes"
```

✅ **ALLOWED after explicit user command:**
```
User: "Commit these changes with message 'Fix authentication bug'"
User: "Push to origin main"
User: "Yes, create that commit"
```

## Enforcement

These rules apply to:
- GitHub Copilot in IDEs (VS Code, Visual Studio, JetBrains, etc.)
- GitHub Copilot CLI (`gh copilot`)
- GitHub Copilot Chat
- Any Copilot-powered tool or extension

**No exceptions unless the user explicitly overrides with a direct command.**