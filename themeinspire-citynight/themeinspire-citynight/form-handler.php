<?php
/**
 * File Name: form-handler.php
 *
 * Send message function to process form submission
 *
 */
if ( isset( $_POST['email'] ) ):

    $name = filter_var( $_POST['name'], FILTER_SANITIZE_STRING );
    $from_email = filter_var( $_POST['email'], FILTER_SANITIZE_EMAIL );
    $from_subject = filter_var( $_POST['subject'], FILTER_SANITIZE_EMAIL );
    $message = filter_var( $_POST['message'], FILTER_SANITIZE_STRING );

    $to_email = "robot@themesinspire.com";    // provide your target email address here
    $to_name = "Jack Miller";

    $email_subject = 'You Have Received a Message From ' . $name . '.';

    if ( ! empty( $subject ) ) {
        $email_subject = $subject . '.';
    }

    $email_body = "You have Received a message from: " . $name . " <br/>";

    $email_body .= "Their additional message is as follows." . " <br/><br/>";
    if (!empty( $from_subject )) {
        $email_body .= "Subject: " . $from_subject . " <br/><br/>";
    }

    $email_content = nl2br( $message ) . " <br/><br/>";

    $email_reply = 	"You can contact " . $name . " via email, " . $from_email ;

    $prepared_message = $email_body . $email_content . $email_reply;

    //https://github.com/eoghanobrien/php-simple-mail for more details
    require 'class.simple_mail.php';

    /* @var SimpleMail $mail */
    $mail = new SimpleMail();
    $mail->setTo( $to_email, $to_name )
        ->setSubject( $email_subject )
        ->setFrom( $from_email, $name )
        ->addMailHeader( 'Reply-To', $from_email, $name )
        ->addGenericHeader( 'X-Mailer', 'PHP/' . phpversion() )
        ->addGenericHeader( 'Content-Type', 'text/html; charset="utf-8"' )
        ->setMessage( $prepared_message );
    $sent = $mail->send();


    if( $sent ) {
        echo json_encode(array(
            "response" => true,
            "message" => "Message Sent Successfully!"
        ));
    } else {
        echo json_encode(array(
                "response" => false,
                "message" => "Server Error:  mail method failed!"
            )
        );
    }

else:

    echo json_encode(array(
            "response" => false,
            "message" => "Invalid Request !"
        )
    );

endif;

die;