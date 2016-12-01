package helpers;

/**
 * Created by zipfs on 2016. 01. 10..
 */


import logger.Log;
import server.ServerVariables;

import javax.mail.Message;
import javax.mail.MessagingException;
import javax.mail.Transport;
import javax.mail.internet.InternetAddress;
import javax.mail.internet.MimeMessage;
import java.util.Properties;


public class Email {

    /*
    Send an email to user
     */
    public static void sendEmail(String to, String messageText, String subject){

        // Get system properties
        Properties properties = System.getProperties();

        // Setup mail server
        properties.setProperty("mail.smtp.host", ServerVariables.getValue(constants.Properties.PROP_DEFAULT_EMAIL_SENDER));

        // Get the default Session object.
        javax.mail.Session session = javax.mail.Session.getDefaultInstance(properties);

        try{
            // Create a default MimeMessage object.
            MimeMessage message = new MimeMessage(session);

            // Set From: header field of the header.
            message.setFrom(new InternetAddress(ServerVariables.getValue(constants.Properties.PROP_DEFAULT_EMAIL_FROM)));

            // Set To: header field of the header.
            message.addRecipient(Message.RecipientType.TO, new InternetAddress(to));

            // Set Subject: header field
            message.setSubject(subject);

            // Now set the actual message
            message.setText(messageText);

            // Send message
            Transport.send(message);
            Log.write("Sent message successfully....");
        }catch (MessagingException ex) {
            Log.write(ex);
        }
    }

}
