using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Input;

namespace CyberShieldBot
{
    public partial class MainWindow : Window
    {
        // MEMORY STORAGE
        private Dictionary<string, string> memory =
            new Dictionary<string, string>();

        // RANDOM RESPONSES
        private Random random = new Random();

        // TEXT TO SPEECH ENGINE
        private SpeechSynthesizer speech = new SpeechSynthesizer();

        private List<string> randomResponses = new List<string>()
        {
             "Interesting cybersecurity concern detected.",
            "Analyzing potential digital threats.",
            "Security protocols verified successfully.",
            "Threat intelligence updated.",
            "Monitoring suspicious activities.",
            "Encryption systems active.",
            "Cyber defense matrix operational."
        };
        public MainWindow()
        {
            InitializeComponent();

            // SPEECH SETTINGS
            speech.Volume = 100;
            speech.Rate = 0;

            // Optional Voice Selection
            // speech.SelectVoice("Microsoft David Desktop");

            BotMessage("🛡 Welcome to CyberShield ChatBot.");
            BotMessage("Voice synthesis activated.");
            BotMessage("Type anything related to cybersecurity.");
        }
        // SEND BUTTON
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessMessage();
        }

        // ENTER KEY
        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessMessage();
            }
        }
        // PROCESS USER MESSAGE
        private void ProcessMessage()
        {
            try
            {
                string userText = UserInput.Text.Trim();

                if (string.IsNullOrWhiteSpace(userText))
                {
                    BotMessage("⚠ Please type a valid message.");
                    return;
                }
                UserMessage(userText);

                string response = GenerateResponse(userText);

                BotMessage(response);

                UserInput.Clear();
            }
            catch (Exception ex)
            {
                BotMessage("❌ System error: " + ex.Message);
            }
        }
        // USER MESSAGE DISPLAY
        private void UserMessage(string message)
        {
            ChatDisplay.AppendText(
                "\nYOU:\n" + message + "\n");

            ChatDisplay.ScrollToEnd();
        }

        // BOT MESSAGE DISPLAY + SPEECH
        private void BotMessage(string message)
        {
            ChatDisplay.AppendText(
                "\nKayla Bot:\n" + message + "\n");

            ChatDisplay.ScrollToEnd();

            Speak(message);
        }
        // TEXT TO SPEECH
        private void Speak(string text)
        {
            try
            {
                speech.SpeakAsyncCancelAll();
                speech.SpeakAsync(text);
            }
            catch
            {
                ChatDisplay.AppendText(
                    "\n[Speech synthesis unavailable]\n");
            }
        }
        // CHATBOT AI ENGINE
        private string GenerateResponse(string input)
        {
            string lowerInput = input.ToLower();
            // MEMORY SYSTEM
            if (lowerInput.Contains("my name is Angela"))
            {
                string name =
                    input.Substring(input.IndexOf("is") + 2).Trim();

                memory["Username"] = name;

                return $"Nice to meet you, {name}. I will remember your identity.";
            }
            if (lowerInput.Contains("what is my name"))
            {
                if (memory.ContainsKey("Angela"))
                {
                    return $"Your name is {memory["Username"]}.";
                }
                else
                {
                    return "Your identity has not yet been registered.";
                }
            }
            // CYBERSECURITY KEYWORDS
            if (lowerInput.Contains("hack"))
            {
                return "⚠ Keep your software and devices updated. Use strong passwords and firewalls. Avoid downloading unknown files or apps. " +
                    "Use antivirus protection and secure Wi-Fi networks.";
            }

            if (lowerInput.Contains("virus"))
            {
                return "🦠 Install reliable antivirus software and scan devices often. Avoid downloading files from unknown websites. " +
                    "Do not open suspicious email attachments. Keep your operating system updated.";
            }

            if (lowerInput.Contains("phishing"))
            {
                return "🎣 Do not click suspicious links or attachments. Check the sender’s email carefully before replying. " +
                    "Never share personal or banking details online. Use spam filters and antivirus software.";
            }
            if (lowerInput.Contains("firewall"))
            {
                return "🔥 Keep your firewall turned on at all times. Update firewall settings regularly. " +
                    "Only allow trusted applications through the firewall. Monitor for unusual network activity.";
            }

            if (lowerInput.Contains("password"))
            {
                return "🔐 Use strong and unique passwords with letters, numbers, and symbols. " +
                    "Avoid sharing passwords with others. Change passwords regularly and enable MFA. " +
                    "Use a trusted password manager.";
            }

            if (lowerInput.Contains("encryption"))
            {
                return "🔒 Use apps and websites with HTTPS encryption. Encrypt sensitive files and devices. " +
                    "Use secure messaging apps. Never share encryption keys or passwords carelessly.";
            }
            if (lowerInput.Contains("vpn"))
            {
                return "🌐 Use a trusted VPN service on public Wi-Fi. Avoid free and untrusted VPNs. " +
                    "Keep the VPN app updated. Disconnect from the VPN when not needed to save speed and battery.";
            }

            // SENTIMENT DETECTION
            if (lowerInput.Contains("sad") ||
                lowerInput.Contains("angry") ||
                lowerInput.Contains("upset"))
            {
                return "Emotional stress detected. Cybersecurity challenges can be resolved methodically.";
            }
            if (lowerInput.Contains("happy") ||
                lowerInput.Contains("awesome") ||
                lowerInput.Contains("great"))
            {
                return "Positive emotional state confirmed. Cyber systems stable.";
            }

            // CONVERSATION FLOW
            if (lowerInput.Contains("hello") ||
                lowerInput.Contains("hi"))
            {
                return "👋 Greetings. CyberShield AI online and ready.";
            }

            if (lowerInput.Contains("how are you"))
            {
                return "🛡 Defense systems fully operational.";
            }
            if (lowerInput.Contains("bye"))
            {
                return "👋 Secure disconnection initiated. Stay protected online.";
            }

            // RANDOM RESPONSE
            return randomResponses[random.Next(randomResponses.Count)];
        }
    }
}
