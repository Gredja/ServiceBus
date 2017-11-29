﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontendWebRole.Models;
using Microsoft.ServiceBus.Messaging;

namespace FrontendWebRole.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Submit");
        }

        public ActionResult Submit()
        {
            // Get a NamespaceManager which allows you to perform management and
            // diagnostic operations on your Service Bus queues.
            var namespaceManager = QueueConnector.CreateNamespaceManager();

            // Get the queue, and obtain the message count.
            var queue = namespaceManager.GetQueue(QueueConnector.QueueName);
            ViewBag.MessageCount = queue.MessageCount;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(OnlineOrder order)
        {
            if (ModelState.IsValid)
            {
                // Create a message from the order.
                var message = new BrokeredMessage(order);

                // Submit the order.
                QueueConnector.OrdersQueueClient.Send(message);
                return RedirectToAction("Submit");
            }
            else
            {
                return View(order);
            }
        }
    }
}