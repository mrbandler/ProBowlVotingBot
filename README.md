# Pro Bowl Voting Bot 🗳️🏈
PBVB - Bot that let's you automate the voting for you favorite pro bowler.

## Table Of Content

1. [Idea](#1-idea) 💡
2. [Usage](#2-usage) ⌨️
2. [Buy me a coffee](#3-buy-me-a-coffee) ☕
3. [License](#4-license) 📃

## 1. Idea

So I came late to American Football, but ever since [Prosieben MAXX](https://www.prosiebenmaxx.de/) started broadcasting [ranNFL](https://www.ran.de/us-sport/nfl) live here in Germany I was hooked!

Why make this bot? Well the guys from [ranNFL](https://www.ran.de/us-sport/nfl) gave a shoutout to vote for our German special teamer **Mark Nzeocha** to make the Pro Bowl. I voted, obviously! But my fingers started to get tired pressing the buttons one after another, so here we are! You can now vote how often you want without doing anything but starting a console application.

## 2. Usage

The console application is pretty simple to use, just download the `ProBowlVotingBot.zip` from the release tab, unzip the folder, start a console in the folder you unzipped the files in and type the following:

```bash
ProBowlVotingBot.exe --position ST --name "mark zeocha"
```

With the above command the bot will download a local Chromium install it and then open it to do it's *magic*!

![ProBowlVotingBot](https://raw.githubusercontent.com/mrbandler/ProBowlVotingBot/master/assets/ProBowlVoteBot.gif)

Of course you can specify any valid position and name from the voting cast. And if you don't need to see what is going on you can hide the browser!

```bash
ProBowlVotingBot.exe --position QB --name "patrick mahomes" --show false
```

**Have fun voting!**  🗳️🏈

## 2. Buy me a coffee

If you like you can buy me a coffee:

[![Support via PayPal](https://cdn.rawgit.com/twolfson/paypal-github-button/1.0.0/dist/button.svg)](https://www.paypal.me/mrbandler/)

[![Donate with Bitcoin](https://en.cryptobadges.io/badge/big/3KGsDx52prxWciBkfNJYBkXaTJ6GUURP2c)](https://en.cryptobadges.io/donate/3KGsDx52prxWciBkfNJYBkXaTJ6GUURP2c)

[![Donate with Ethereum](https://en.cryptobadges.io/badge/big/0xd6Ffc89Bc87f7dFdf0ef1aefF956634d4B7451c8)](https://en.cryptobadges.io/donate/0xd6Ffc89Bc87f7dFdf0ef1aefF956634d4B7451c8)

---

## 3. License

MIT License

Copyright (c) 2018 fivefingergames

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.