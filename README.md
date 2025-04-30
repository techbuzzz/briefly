# 🧠 Briefly — AI-Powered Team Digest Assistant

**Briefly** is an open-source tool that helps teams collect and summarize their daily syncs, grooming notes, meeting minutes, and fortnightly updates — and turns them into clean, AI-generated HTML digests and podcast-style voice summaries.

Powered by **Azure OpenAI (GPT-4o)** and **Azure Cognitive Services (Text-to-Speech)**, Briefly automates your internal updates and makes them accessible, beautiful, and easy to consume.

---

## ✨ Features (Planned and Ongoing)

- 🛠️ Collect updates from team members via UI and WebHooks (Microsoft Forms or API)
- 🛠️ Store and filter data in Supabase
- 🛠️ Store and filter data in SharePoint Online
- 🛠️ Trigger digest generation with Power Automate or schedule it
- 🛠️ Use Azure Function + GPT-4o to generate structured HTML digests
- 🛠️ Generate summary for voice conversion
- 🛠️ Use Azure Text-to-Speech to produce a short internal podcast
- 🛠️ Web dashboard to submit and browse team updates
- 🛠️ Categorized sections in digest: Highlights, Dev, QA, Risks
- 🛠️ Mood analysis and emoji indicator for the team state
- 🛠️ Frontend in React/Next.js or Blazor
- 🛠️ Admin view for digest control and history
- 🛠️ Export/share/email digests

---

## 🔭 Roadmap (Q2 2025)

### Milestone 1 — 🚀 MVP Setup
- [ ] Initial structure 
- [ ] Initial backend .Net Aspire via Azure Function (.NET)
- [ ] Initial frontend OfficeUI and Blazor (.NET)
- [ ] First working prompt for GPT-4o HTML generation
- [ ] Sample JSON input and digest output
- [ ] Trigger via Power Automate
- [ ] Deployable Azure setup with Function + OpenAI resource

### Milestone 2 — 🗂️ Data Integration
- [ ] Microsoft Forms integration with SharePoint Online
- [ ] Filtering updates by date/week
- [ ] SharePoint GetItems filter for current week
- [ ] Webhook endpoint for external ingestion (optionally Supabase)

### Milestone 3 — 🎨 Frontend
- [ ] Minimal UI to submit status update manually
- [ ] View digest history and playback audio
- [ ] Admin settings for scheduling, prompts, templates

### Milestone 4 — 🔉 Voice Digest
- [ ] Generate TTS via Azure Text-to-Speech
- [ ] Embed playable audio in HTML/email
- [ ] Optional download/podcast-style feed

### Milestone 5 — 🚀 Launch Preview
- [ ] Setup GitHub Pages demo site
- [ ] Prepare open-source launch
- [ ] Documentation and use case examples

---

## 🛠️ Tech Stack

- **Backend:** Azure Functions (.NET), C#
- **AI Engine:** Azure OpenAI GPT-4o
- **Voice Engine:** Azure Cognitive Services (TTS)
- **Orchestration:** Power Automate
- **Storage:** SharePoint Online / Supabase (alternative)
- **Frontend:** React + Tailwind CSS / Blazor (planned)

---

## 📄 License

This project is licensed under the **MIT License** — see the [LICENSE](./LICENSE) file for details.

---

## 🤝 Contributing

We welcome contributions! To get started:
- Fork the repo
- Read our [CONTRIBUTING.md](./CONTRIBUTING.md)
- Suggest new ideas in Discussions or Issues

> Let’s make team communication smarter — one digest at a time 💡

