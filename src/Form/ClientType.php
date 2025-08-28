<?php

namespace App\Form;

use App\Entity\Client;
use Symfony\Component\Form\AbstractType;
use Symfony\Component\Form\Extension\Core\Type\EmailType;
use Symfony\Component\Form\Extension\Core\Type\PasswordType;
use Symfony\Component\Form\Extension\Core\Type\RepeatedType;
use Symfony\Component\Form\Extension\Core\Type\SubmitType;
use Symfony\Component\Form\Extension\Core\Type\TextType;
use Symfony\Component\Form\FormBuilderInterface;
use Symfony\Component\OptionsResolver\OptionsResolver;

use Symfony\Component\HttpFoundation\Reques;



class ClientType extends AbstractType
{
    public function buildForm(FormBuilderInterface $builder, array $options): void
    {
        $builder
            ->add('firstname', TextType::class, [
                'attr' => ['placeholder'=>'Please your firstname'],
                'label' => 'Your firstname :',
                ])
            ->add('lastname', TextType::class, [
                'attr' => ['placeholder'=>'Please your lastname'],
                'label' => 'Your lastname :',
                ])
            ->add('age', TextType::class, [
                'attr' => ['placeholder'=>'Please your age'],
                'label' => 'Your age :',
                ])
            ->add('address', TextType::class, [
                'attr' => ['placeholder'=>'Please your address'],
                'label' => 'Your address :',
                ])
            ->add('city', TextType::class, [
                'attr' => ['placeholder'=>'Please your city'],
                'label' => 'Your city :',
                ])
            ->add('phone', TextType::class, [
                'attr' => ['placeholder'=>'Please your phone'],
                'label' => 'Your phone :',
                ])
            // ->add('roles', TextType::class, [
            //     'attr' => ['placeholder'=>'Please your Country'],
            //     'label' => 'Your role :',
            //     ])
            ->add('email', EmailType::class, [
                'attr' => ['placeholder'=>'Please your email'],
                'label' => 'Your email :',
                ])
            ->add('password', RepeatedType::class, [
                'type'=>PasswordType::class,
                'invalid_message' => 'the password moste be the same.',
                'attr' => ['placeholder'=>'Please your password'],
                'required'=>true,
                'first_options'=>['label' => 'Your password :']
                ,
                'second_options'=>['label' => 'Confime your password :']
                ])
                
            ->add('submit', SubmitType::class, [
                    'label' => 'Send',
                    ])
            
        ;
    }

    public function configureOptions(OptionsResolver $resolver): void
    {
        $resolver->setDefaults([
            'data_class' => Client::class,
        ]);
    }
}
